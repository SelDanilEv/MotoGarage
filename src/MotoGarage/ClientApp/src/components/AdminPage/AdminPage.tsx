import * as React from "react";
import { forwardRef, useContext, useEffect, useState } from "react";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";
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

const AdminPage = () => {

  enum Roles {
    Admin = 'Admin',
    Employee = 'Employee',
    Client = 'Client',
  }
  
  const sortedRoles = Object.values(Roles);
  
  const rolesDropList : object = { 'Client': 'Client', 'Employee': 'Employee', "Admin": 'Admin' };

  const columnsInit: Column<any>[] =
    [
      { title: "id", field: "id", hidden: true },
      { title: "Name", field: "name" },
      { title: "Email", field: "email", editable: 'never' },
      {
        title: 'Role',
        field: 'role',
        lookup: rolesDropList,
        defaultGroupOrder: 0,
        customSort: (a, b) => sortedRoles.indexOf(a) - sortedRoles.indexOf(b),
      },
    ]

  const [columns, setColumns] = useState(columnsInit);

  const [data, setData] = useState(Array<any>());
  const [iserror, setIserror] = useState(false);
  const [errorMessages, setErrorMessages] = useState(Array<string>());

  const [loadingState, setLoadingState] = useContext(LoadingContext);

  useEffect(() => {
    refreshData();
  }, [])

  const refreshData = () => {
    wrapAPICall(async () => {
      const response = await fetch("api/AccountManager/GetAllUsers", {
        method: "GET",
      });

      const result = await response.json();

      switch (response.status) {
        case 200:
          setData(result);
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

  const handleRowUpdate = (newData: any, oldData: any, resolve: any) => {
    //validation
    let errorList = []
    if (newData.name === "") {
      errorList.push("Please enter name")
    }
    
    if (newData.role !== "Admin" && newData.role !== "Employee" && newData.role !== "Client") {
      newData.role = oldData.role;
      errorList.push("Please enter a valid role")
    }

    if (errorList.length < 1) {
      wrapAPICall(async () => {
        const response = await fetch("api/AccountManager/UpdateUser", {
          method: "POST",
          body: JSON.stringify(newData),
          headers: {
            "Content-Type": "application/json",
          },
        });

        const result = await response.json();

        switch (response.status) {
          case 200:
            const dataUpdate = [...data];
            const index = oldData.tableData.id;
            dataUpdate[index] = newData;
            setData([...dataUpdate]);
            resolve()
            setIserror(false)
            setErrorMessages([])
            break;
          case 400:
          default:
            setErrorMessages(["Update failed! Server error"])
            setIserror(true)
            resolve()
        }
      }, setLoadingState);
    } else {
      setErrorMessages(errorList)
      setIserror(true)
      resolve()
    }

  }

  const handleRowDelete = (oldData: any, resolve: any) => {
    wrapAPICall(async () => {
      const response = await fetch("api/AccountManager/RemoveUser?id=" + oldData.id, {
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
        default:
          setErrorMessages(["Delete failed! Server error"])
          setIserror(true)
          resolve()
      }
    }, setLoadingState);
  }

  return (
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
        <MaterialTable
          title="User list"
          columns={columns}
          data={data}
          icons={tableIcons}
          style={{
            width: 'fit-content',
            minWidth: '50vw',
            margin: '0 auto'
          }}
          editable={{
            onRowUpdate: (newData, oldData) =>
              new Promise((resolve) => {
                handleRowUpdate(newData, oldData, resolve);
              }),
            // onRowAdd: (newData) =>
            //   new Promise((resolve) => {
            //     handleRowAdd(newData, resolve)
            //   }),
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
            }
          ]}
          options={{
            actionsColumnIndex: -1,
            sorting: true,
            grouping: true,
          }}
        />
      </Grid>
    </Grid>
  );
};

export default AdminPage;
