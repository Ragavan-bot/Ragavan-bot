import React, { useState, useEffect } from "react";
import Header from "../Header/Header";
import DataGrid, {
  Column,
  ColumnChooser,
  Editing,
  Lookup,
} from "devextreme-react/data-grid";
import {
  DeleteAPICall,
  notifyMessage,
  postAPICall,
  putAPICall,
} from "../../api/api";

export default function Master() {
  const [data, setData] = useState([]);
  const [userTypeData, setUserTypeData] = useState([]);
  useEffect(() => {
    fetchData();
    fetchDataUserType();
  }, []);
  const fetchData = async () => {
    debugger;
    try {
      const requestBody = {
        "": "",
      };
      const response = await postAPICall(
        "api/PortalLogin/GetUserCreation",
        requestBody
      );
      setData(response?.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };
  const fetchDataUserType = async () => {
    debugger;
    try {
      const requestBody = {
        "": "",
      };
      const response = await postAPICall(
        "api/PortalLogin/GetUserType",
        requestBody
      );
      console.log(response?.data);
      setUserTypeData(response?.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const userAdd = async (e) => {
    debugger;
    let userNew = e.data;

    let userName = userNew.Username;
    let password = userNew.Password;
    let mailAddress = userNew.MailAddress;
    let mobileNumber = userNew.MobileNumber;
    let userTypeID = userNew.UserTypeId;

    const requestBody = {
      userName: userName,
      password: password,
      mailAddress: mailAddress,
      mobileNumber: mobileNumber,
      userTypeID: userTypeID,
    };
    const response = await postAPICall(
      "api/PortalLogin/PostUserCreation",
      requestBody
    );
    //debugger;
    //console.log(response.data);
    notifyMessage(response.data[0].Result, "success");
  };

  const userUpdate = async (e) => {
    debugger;
    const userNew = e.key;
    let UserID = userNew.UserID;
    let userName = e.newData.Username ? e.newData.Username : userNew.Username;
    let password = e.newData.Password ? e.newData.Password : userNew.Password;
    let mailAddress = e.newData.MailAddress
      ? e.newData.MailAddress
      : userNew.MailAddress;
    let mobileNumber = e.newData.MobileNumber
      ? e.newData.MobileNumber
      : userNew.MobileNumber;
    let userTypeID = e.newData.UserTypeId
      ? e.newData.UserTypeId
      : userNew.UserTypeId;

    const requestBody = {
      UserID: UserID,
      userName: userName,
      password: password,
      mailAddress: mailAddress,
      mobileNumber: mobileNumber,
      userTypeID: userTypeID,
    };
    const response = await putAPICall(
      "api/PortalLogin/PutUserCreation",
      requestBody
    );
    //debugger;
    //console.log(response.data);
    notifyMessage(response.data[0].Result, "success");
  };
  const userDelete = async (e) => {
    let UserID = e.data.UserID;

    console.log("UserId", UserID);

    const requestBody = {
      userID: UserID,
    };
    const response = await DeleteAPICall(
      "api/PortalLogin/DeleteUserType",
      requestBody
    );
    // debugger;
    // console.log(response.data);
    notifyMessage(response.data[0].Result, "success");
  };

  return (
    <div>
      <Header />
      <DataGrid
        dataSource={data}
        height={500}
        onRowInserting={userAdd}
        onRowRemoving={userDelete}
        onRowUpdating={userUpdate}
      >
        <Editing allowAdding={true} allowDeleting={true} allowUpdating={true} />
        <ColumnChooser enabled={true} />
        <Column dataField="UserID" />
        <Column dataField="Username" />
        <Column dataField="Password" />
        <Column dataField="MailAddress" />
        <Column dataField="MobileNumber" />
        <Column dataField="UserTypeId" caption="User Type">
          <Lookup
            dataSource={userTypeData}
            displayExpr="UserType"
            valueExpr="UserTypeId"
          />
          {/* <Lookup dataSource={states} displayExpr="Name" valueExpr="ID" /> */}
        </Column>
      </DataGrid>
    </div>
  );
}
