import * as React from "react";
import { forwardRef, useContext, useEffect, useState } from "react";
import { LoadingContext } from "../GlobalState/LoadingState/LoadingStore";
import wrapAPICall from "../GlobalState/LoadingState/wrapAPICall";

import MaterialTable from "material-table";
import AddBox from '@material-ui/icons/AddBox';
import ArrowDownward from '@material-ui/icons/ArrowDownward';
import Check from '@material-ui/icons/Check';
import ChevronLeft from '@material-ui/icons/ChevronLeft';
import ChevronRight from '@material-ui/icons/ChevronRight';
import Clear from '@material-ui/icons/Clear';
import DeleteOutline from '@material-ui/icons/DeleteOutline';
import Edit from '@material-ui/icons/Edit';
import FilterList from '@material-ui/icons/FilterList';
import FirstPage from '@material-ui/icons/FirstPage';
import LastPage from '@material-ui/icons/LastPage';
import Remove from '@material-ui/icons/Remove';
import SaveAlt from '@material-ui/icons/SaveAlt';
import Search from '@material-ui/icons/Search';
import ViewColumn from '@material-ui/icons/ViewColumn';

const tableIcons = {
  Add: forwardRef((props, ref) => <AddBox {...props} ref={ref} />),
  Check: forwardRef((props, ref) => <Check {...props} ref={ref} />),
  Clear: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
  Delete: forwardRef((props, ref) => <DeleteOutline {...props} ref={ref} />),
  DetailPanel: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
  Edit: forwardRef((props, ref) => <Edit {...props} ref={ref} />),
  Export: forwardRef((props, ref) => <SaveAlt {...props} ref={ref} />),
  Filter: forwardRef((props, ref) => <FilterList {...props} ref={ref} />),
  FirstPage: forwardRef((props, ref) => <FirstPage {...props} ref={ref} />),
  LastPage: forwardRef((props, ref) => <LastPage {...props} ref={ref} />),
  NextPage: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
  PreviousPage: forwardRef((props, ref) => <ChevronLeft {...props} ref={ref} />),
  ResetSearch: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
  Search: forwardRef((props, ref) => <Search {...props} ref={ref} />),
  SortArrow: forwardRef((props, ref) => <ArrowDownward {...props} ref={ref} />),
  ThirdStateCheck: forwardRef((props, ref) => <Remove {...props} ref={ref} />),
  ViewColumn: forwardRef((props, ref) => <ViewColumn {...props} ref={ref} />)
};

const AdminPage = () => {
  var columns = [
    { title: "id", field: "id", hidden: true },
    { title: "Name", field: "name" },
    { title: "Email", field: "email" },
    { title: "Role", field: "role" }
  ]

  const [data, setData] = useState([]); //table data
  //for error handling
  const [iserror, setIserror] = useState(false);
  const [errorMessages, setErrorMessages] = useState([]);
  const [loadingState, setLoadingState] = useContext(LoadingContext);

  useEffect(() => {
    wrapAPICall(async () => {
      const response = await fetch("api/AccountManager/GetAllUsers", {
        method: "GET",
      });

      const result = await response.json();
      console.log(result);

      switch (response.status) {
        case 200:
          setData(result);
          console.log("Users loaded");
          break;
        case 400:
          console.log("Validation error");
        default:
          console.log("Default");
          setIserror(true)
      }
    }, setLoadingState);
  }, [])

  const handleRowUpdate = (newData, oldData, resolve) => {
    //validation
    let errorList = []
    if (newData.name === "") {
      errorList.push("Please enter name")
    }

    newData.Email = oldData.Email;

    if (newData.Role !== "Admin" && newData.Role !== "Employee" && newData.Role !== "Client") {
      newData.Role = oldData.Role;
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
        console.log(result);

        switch (response.status) {
          case 200:
            const dataUpdate = [...data];
            const index = oldData.tableData.id;
            dataUpdate[index] = newData;
            setData([...dataUpdate]);
            resolve()
            setIserror(false)
            setErrorMessages([])
            console.log("Users loaded");
            break;
          case 400:
            console.log("Validation error");
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

  const handleRowDelete = (oldData, resolve) => {
    wrapAPICall(async () => {
      const response = await fetch("api/AccountManager/RemoveUser?id="+ oldData.id, {
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

  return (
    <MaterialTable
      title="User list from API"
      columns={columns}
      data={data}
      icons={tableIcons}
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
    />
  );
};

export default AdminPage;
