import React, { useState, useEffect } from "react";
import "devextreme/dist/css/dx.light.css";
import DataGrid, {
  Column,
  Editing,
  Paging,
  Popup,
  Selection,
  SearchPanel,
  RowDragging,
} from "devextreme-react/data-grid";
import { ColumnChooser } from "devextreme-react/tree-list";
import { DeleteAPICall, notifyMessage, postAPICall } from "../../api/api";
import "../Playlist/Playlist.css";
import { Validator, RequiredRule } from "devextreme-react/validator";
import { ToolbarItem } from "devextreme-react/popup";
import TextBox from "devextreme-react/text-box";
import Button from "devextreme-react/button";
import '../Schedule/Schedule.css'
import { LoadPanel } from "devextreme-react";
import { useNavigate } from "react-router-dom"

export default function PlaylistScheduleHeader(props) {
  const [PlaylistScheduleHead, setPlaylistScheduleHead] = useState([]);
  //loader
  const [loading, setLoading] = useState(false);
    // Function to show the loading panel
  const showLoadingPanel = () => {
    setLoading(true);
  };
  // Function to hide the loading panel
  const hideLoadingPanel = () => {
    setLoading(false);
  };
  // Simulate a loading operation
 const simulateLoading = () => {
    showLoadingPanel();
    // Simulate some asynchronous operation (e.g., API call, setTimeout)
    setTimeout(() => {
      hideLoadingPanel();
    }); // Simulate loading for 3 seconds
  };

  useEffect(() => {
    props?.selectednewid && fetchPlaylistScheduleHead();
    simulateLoading();
  }, [props]);

  const [text, setText] = useState(null);
  const [popupVisible, setPopupVisible] = useState(false);
  const [schadd, setschadd] = useState(false);
  const [schdrag, setschdrag] = useState(false);

  const fetchPlaylistScheduleHead = async () => {
    let stringconversion = props?.selectednewid.toString() || "";
    let stringModuleName = props?.selectednewName.toString() || "";
    //schedule add button Visivle false
    stringModuleName === "Schedule" ? setschadd(false) : setschadd(true);
    stringModuleName === "Schedule"
      ? setschdrag("schgroup")
      : setschdrag(false);

    const requestBody = {
      companyID: stringconversion,
    };
    const response = await postAPICall(
      "api/PlayLists/GetScheduleHeader",
      requestBody
    );

    setPlaylistScheduleHead(response?.data);
  };
  function getPlaylistScheduleHeadID(selectedRowsData) {
    if (selectedRowsData.length > 0) {
      return selectedRowsData.map(
        (selectedValue) => selectedValue.ScheduleHdrId
      );
    }
  }
  const onSelectionChange = (e) => {
    let getID = getPlaylistScheduleHeadID(e.selectedRowsData);
    if (getID != undefined) props.onclickcheck(getID);
  };
  //cancel useeffect
  useEffect(() => {
    setPopupVisible(false);
  }, [popupVisible]);
  const hideInfo = () => {
    setPopupVisible(true);
  };
  //textbox value
  const SchedulenameChange = (ScheduleHeaderName) => {
    setText(ScheduleHeaderName);
  };

  //textbox suubmit
  const onEnterKey = async () => {
    let textbox = text;
    if (textbox !== "" && textbox != null) {
      //post api
      const requestBody = {
        companyID: "1",
        scheduleHeaderName: text,
      };
      const response = await postAPICall(
        "api/PlayLists/PostScheduleHeader",
        requestBody
      );
    }
    fetchPlaylistScheduleHead();
  };
  //toastr
  const onFormSubmit = (e) => {
    e.preventDefault();
    notifyMessage("Schedule Header added..", "success");

    hideInfo();
    setText("");
  };
  //popup text set
  const renderContent = () => {
    return (
      <>
        <br></br>
        <form onSubmit={onFormSubmit} id="formcontent">
          <TextBox
            placeholder="Schedulename"
            value={text}
            onValueChange={SchedulenameChange}
            width={250}
          >
            <Validator>
              <RequiredRule message="Schedulename is required" />
            </Validator>
          </TextBox>

          <Button
            onClick={onEnterKey}
            id="btnsave"
            text="Save"
            type="success"
            useSubmitBehavior={true}
          />
          <Button
            text="Cancel"
            type="danger"
            id="btncancel"
            onClick={hideInfo}
          />
        </form>
        <br></br>
      </>
    );
  };

  const scheduleheaderclick = async (e) => {
    const requestBodydelete = {
      companyID: "1",
      scheduleHeaderID: e.data.ScheduleHdrId,
    };
    const response = await DeleteAPICall(
      "api/PlayLists/DeleteScheduleHeader",
      requestBodydelete
    );
    notifyMessage("Schedule header Deleted..", "success");
    console.log(response?.data);
  };
  const selectChange = (e) => {
    if (props.selectednewName != "Schedule") {
      onSelectionChange(e);
    }
  };
     //Row Double click to navigate
  let navigate = useNavigate();
  const onRowDblClick=(e)=>{
  let stringconversion =e.data.ScheduleHdrId;
  localStorage.setItem("SchId",stringconversion)
  navigate("/playlist")
  }
  return (
    <div>
       <LoadPanel
        // shading={true}
        // shadingColor="rgba(0,0,0,0.4)"
        visible={loading}
        showIndicator={true}
        showPane={true}
      />
      <DataGrid   
        dataSource={PlaylistScheduleHead}
        keyExpr="ScheduleHdrId"
        showBorders={true}
        id="ScheduleHeaderGrid"
        onRowRemoving={scheduleheaderclick}
        onSelectionChanged={selectChange}
        onRowDblClick={onRowDblClick}
      >
        <RowDragging group={schdrag} />
        <SearchPanel  visible={true} highlightCaseSensitive={true} />
        <Selection mode="single" />
        <Column
          dataField="Schedulename"
          visible={true}
          width={true}
          alignment="left"
        />
        <Column
          dataField="Duration"
          visible={true}
          width={70}
          minWidth={true}
          dataType="timespan"
        />
        {/* <ColumnChooser enabled={true} /> */}
        <Paging enabled={true}></Paging>

        <Editing mode="popup" allowDeleting={schadd} allowAdding={schadd}>
          <Popup
            width={500}
            height={250}
            contentRender={renderContent}
            resizeEnabled={false}
            hideOnOutsideClick={false}
            showCloseButton={true}
            title="ScheduleName"
            showTitle={true}
            visible={popupVisible}
          >
            <ToolbarItem
              toolbar="bottom"
              widget=""
              location="after"
            ></ToolbarItem>
          </Popup>
        </Editing>
      </DataGrid>
    </div>
  );
}
