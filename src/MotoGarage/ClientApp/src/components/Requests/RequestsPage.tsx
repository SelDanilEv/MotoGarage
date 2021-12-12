import * as React from "react";
import { forwardRef, useContext, useEffect, useState } from "react";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
import CreateRequestPopup from "../Popup/Templates/CreateRequestPopup";
import Popup from "../Popup/Popup";
import RequestDetails from "./RequestDetails";
import { CurrentUserContext } from "../GlobalState/CurrentUser/CurrentUserStore";
import { Alert, Grid } from "@mui/material";
import MaterialTable, { Column, Icons } from "material-table";

import {
  AddBox,
  ArrowDownward,
  Check,
  ChevronLeft,
  ChevronRight,
  DeleteOutline,
  Edit,
  Refresh,
  Clear,
  FilterList,
  FirstPage,
  LastPage,
  Remove,
  SaveAlt,
  Search,
  ViewColumn,
  Input
} from '@material-ui/icons';


const tableIcons: Icons = {
  Add: forwardRef((props: any, ref: any) => <AddBox {...props} ref={ref} />),
  Check: forwardRef((props: any, ref: any) => <Check {...props} ref={ref} />),
  Clear: forwardRef((props: any, ref: any) => <Clear {...props} ref={ref} />),
  Delete: forwardRef((props: any, ref: any) => <DeleteOutline {...props} ref={ref} />),
  DetailPanel: forwardRef((props: any, ref: any) => <ChevronRight {...props} ref={ref} />),
  Edit: forwardRef((props: any, ref: any) => <Edit {...props} ref={ref} />),
  Export: forwardRef((props: any, ref: any) => <SaveAlt {...props} ref={ref} />),
  Filter: forwardRef((props: any, ref: any) => <FilterList {...props} ref={ref} />),
  FirstPage: forwardRef((props: any, ref: any) => <FirstPage {...props} ref={ref} />),
  LastPage: forwardRef((props: any, ref: any) => <LastPage {...props} ref={ref} />),
  NextPage: forwardRef((props: any, ref: any) => <ChevronRight {...props} ref={ref} />),
  PreviousPage: forwardRef((props: any, ref: any) => <ChevronLeft {...props} ref={ref} />),
  ResetSearch: forwardRef((props: any, ref: any) => <Clear {...props} ref={ref} />),
  Search: forwardRef((props: any, ref: any) => <Search {...props} ref={ref} />),
  SortArrow: forwardRef((props: any, ref: any) => <ArrowDownward {...props} ref={ref} />),
  ThirdStateCheck: forwardRef((props: any, ref: any) => <Remove {...props} ref={ref} />),
  ViewColumn: forwardRef((props: any, ref: any) => <ViewColumn {...props} ref={ref} />)
};


import "./../../assets/mui_table/style.css";


const RequestsPage = () => {

  const statusesDropList: any = {
    0: "Triage",
    1: "To do",
    2: "In progress",
    3: "Needs details",
    4: "Ready to check",
    5: "Done",
    6: "Closed",
  };

  const columnsInit: Column<any>[] = [
    { title: "id", field: "id", hidden: true },
    { title: "Title", field: "title", editable: "never" },
    { title: "Assignee", field: "assignee_person", editable: "never" },
    { title: 'Status', field: 'status', lookup: statusesDropList }
  ]
  const [columns, setColumns] = useState(columnsInit);
  const [data, setData] = useState(Array<any>());
  const [iserror, setIserror] = useState(false);
  const [showCreateRequestPopup, setShowCreateRequestPopup] = useState(false);
  const [errorMessages, setErrorMessages] = useState(Array<string>());
  const [loadingState, setLoadingState] = useContext(LoadingContext);
  const [currentUserState, setCurrentUserState]: any = useContext(CurrentUserContext);

  useEffect(() => {
    if (!showCreateRequestPopup) {
      refreshData();
    }
  }, [showCreateRequestPopup])

  const refreshData = () => {
    wrapAPICall(async () => {
      const response = await fetch("api/ServiceRequest/GetUserRequests", {
        method: "GET",
      });

      const result = await response.json();
      console.log(result);

      switch (response.status) {
        case 200:
          result.getData = result.getData.map((elem: any) => {
            elem.assignee ?
              elem.assignee_person = `${elem.assignee.name}(${elem.assignee.email})` :
              elem.assignee_person = `Not assigned`
            return elem;
          })
          setData(result.getData);
          break;
        case 400:
        default:
          let errorList: string[] = [];
          errorList.push("Server error!!!")
          setErrorMessages(errorList)
          setIserror(true)
      }
    }, setLoadingState);
  }

  const handleRowDelete = (oldData: any, resolve: any) => {
    wrapAPICall(async () => {
      const response = await fetch("api/ServiceRequest/RemoveServiceRequest?guid=" + oldData.id, {
        method: "POST"
      });

      switch (response.status) {
        case 200:
          const dataDelete = [...data];
          const index = oldData.tableData.id;
          dataDelete.splice(index, 1);
          setData([...dataDelete]);
          resolve()
          break;

        case 400:
          console.log("Validation error");
        default:
          setErrorMessages(["Delete failed! Server error"])
          setIserror(true)
          resolve()
      }
    }, setLoadingState);

  }

  const handleCreateRequestPopup = () => {
    setShowCreateRequestPopup(true);
  }

  const renderDetailsGrid = () => {
    switch (currentUserState.CurrentUser?.role) {
      case "Admin":
        return <MaterialTable
          title="Request list"
          columns={columns}
          data={data}
          icons={tableIcons}
          style={{
            width: 'fit-content',
            minWidth: '50vw',
            margin: '0 auto'
          }}
          editable={{
            onRowDelete: (oldData) =>
              new Promise((resolve) => {
                handleRowDelete(oldData, resolve)
              }),
          }}
          actions={[
            {
              icon: AddBox,
              tooltip: 'Create new request',
              isFreeAction: true,
              onClick: handleCreateRequestPopup
            },
            {
              icon: Refresh,
              tooltip: 'Refresh Data',
              isFreeAction: true,
              onClick: () => refreshData()
            },
          ]}
          options={{
            actionsColumnIndex: -1,
            sorting: true,
            grouping: true,
          }}
          cellEditable={{
            onCellEditApproved: (newValue, oldValue, rowData, columnDef) => {
              return new Promise((resolve, reject) => {
                wrapAPICall(async () => {
                  rowData.status = +newValue;

                  const requestData = {
                    RequestId: rowData.id,
                    NewStatus: +newValue,
                  };

                  const response = await fetch("api/ServiceRequest/ChangeStatus", {
                    method: "POST",
                    body: JSON.stringify(requestData),
                    headers: {
                      "Content-Type": "application/json",
                    },
                  });

                  const result = await response.json();
                  console.log(result);

                  switch (response.status) {
                    case 200:
                      setData(data.map(row => {
                        if (row.id == rowData.id) {
                          row.status = newValue;
                        }

                        return row
                      }))
                      resolve()
                      console.log("Users loaded");
                      break;
                    case 400:
                      console.log("Validation error");
                    default:
                  }
                }, setLoadingState);
              });
            }
          }}
          detailPanel={rowData => {
            return (
              <RequestDetails item={rowData} refreshData={refreshData} />
            )
          }} />
      case "Employee":
        return <MaterialTable
          title="Request list"
          columns={columns}
          data={data}
          icons={tableIcons}
          style={{
            width: 'fit-content',
            minWidth: '50vw',
            margin: '0 auto'
          }}
          editable={{
            onRowDelete: (oldData) =>
              new Promise((resolve) => {
                handleRowDelete(oldData, resolve)
              }),
          }}
          actions={[
            {
              icon: Refresh,
              tooltip: 'Refresh Data',
              isFreeAction: true,
              onClick: () => refreshData()
            },
          ]}
          options={{
            actionsColumnIndex: -1,
            sorting: true,
            grouping: true,
          }}
          cellEditable={{
            onCellEditApproved: (newValue, oldValue, rowData, columnDef) => {
              return new Promise((resolve, reject) => {
                wrapAPICall(async () => {
                  rowData.status = +newValue;

                  const requestData = {
                    RequestId: rowData.id,
                    NewStatus: +newValue,
                  };

                  const response = await fetch("api/ServiceRequest/ChangeStatus", {
                    method: "POST",
                    body: JSON.stringify(requestData),
                    headers: {
                      "Content-Type": "application/json",
                    },
                  });

                  const result = await response.json();
                  console.log(result);

                  switch (response.status) {
                    case 200:
                      setData(data.map(row => {
                        if (row.id == rowData.id) {
                          row.status = newValue;
                        }

                        return row
                      }))
                      resolve()
                      console.log("Users loaded");
                      break;
                    case 400:
                      console.log("Validation error");
                    default:
                  }
                }, setLoadingState);
              });
            }
          }}
          detailPanel={rowData => {
            return (
              <RequestDetails item={rowData} />
            )
          }} />
      case "Client":
        return <MaterialTable
          title="Request list"
          columns={columns}
          data={data}
          icons={tableIcons}
          style={{
            width: 'fit-content',
            minWidth: '50vw',
            margin: '0 auto'
          }}
          actions={[
            {
              icon: AddBox,
              tooltip: 'Create new request',
              isFreeAction: true,
              onClick: handleCreateRequestPopup
            },
            {
              icon: Refresh,
              tooltip: 'Refresh Data',
              isFreeAction: true,
              onClick: () => refreshData()
            },
          ]}
          options={{
            actionsColumnIndex: -1,
            sorting: true,
            grouping: true,
          }}
          detailPanel={rowData => {
            return (
              <RequestDetails item={rowData} refreshData={refreshData} />
            )
          }} />
    }
  }

  return (
    <>
      {
        showCreateRequestPopup ?
          <CreateRequestPopup setShowPopup={setShowCreateRequestPopup} />
          : null
      }
      <Grid container
        spacing={1}
        position='absolute'
        top='100px'>
        <Grid xs={12}>
          <div>
            {
              iserror &&
              <Alert severity="error">
                {errorMessages.map((msg, i) => {
                  return <div key={i}>{msg}</div>
                })}
              </Alert>
            }
          </div>
          {renderDetailsGrid()}
        </Grid>
      </Grid>
    </>
  )
}

export default RequestsPage;

