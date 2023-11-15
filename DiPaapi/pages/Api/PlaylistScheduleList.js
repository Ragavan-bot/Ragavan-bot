import React, { useState, useEffect } from "react";
import "devextreme/dist/css/dx.light.css";
import DataGrid, {
  Selection,
  Column,
  ColumnChooser,
  Summary,
  TotalItem,
  RowDragging,
  Editing,
  gridRef,
} from "devextreme-react/data-grid";
import {
  DeleteAPICall,
  notifyMessage,
  postAPICall,
  putAPICall,
  s3url,
} from "../../api/api";
import Modal from 'react-modal';
import { LoadPanel } from "devextreme-react";

export default function PlaylistScheduleList(props) {
  const [PlaylistSchList, setPlaylistScheduleList] = useState([]);
  const [TotalDuration, setTotalDuration] = useState("00:00:00");
  const [location, SetVideoPath] = useState("");
  const [modalIsOpen, setModalIsOpen] = useState(false);

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
    // props?.selectednewid != "" && fetchScheduleList();
    props?.selectednewid != ""
      ? fetchScheduleList()
      : localStorage.getItem("SchId") != null && fetchScheduleList();
      simulateLoading();
  }, [props]);
  const fetchScheduleList = async () => {
    debugger;
    // let stringconversion = props?.selectednewid.toString() || "";
    let stringconversion =
      props?.selectednewid != ""
        ? props?.selectednewid.toString()
        : localStorage.getItem("SchId") != null
        ? localStorage.getItem("SchId").toString()
        : "";
       localStorage.removeItem("SchId"); 
    const requestBody = {
      scheduleHeaderID: stringconversion,
    };

    const response = await postAPICall(
      "api/PlayLists/GetScheduleList",
      requestBody
    );
    setPlaylistScheduleList(response?.data);

    var data = response.data;
    // Calculate the sum of time durations
    const sum = new Date(0, 0, 0, 0); // Initialize with zero time
    for (let i = 0; i < data.length; i++) {
      const [hours, minutes, seconds] = data[i].MediaDuration.split(":");

      // Create a new Date object with today's date and the parsed time
      const date = new Date();
      date.setHours(hours);
      date.setMinutes(minutes);
      date.setSeconds(seconds);
      sum.setHours(sum.getHours() + date.getHours());
      sum.setMinutes(sum.getMinutes() + date.getMinutes());
      sum.setSeconds(sum.getSeconds() + date.getSeconds());
    }
    let TDHours = formatNumber(`${sum.getHours()}`);
    let TDMinitues = formatNumber(`${sum.getMinutes()}`);
    let TDSeconds = formatNumber(`${sum.getSeconds()}`);
    let TotalDurationtime = `${TDHours}:${TDMinitues}:${TDSeconds}`;
    setTotalDuration(TotalDurationtime);
  };
  const formatNumber = (number) => {
    return number < 10 ? `0${number}` : `${number}`;
  };
  const onReorder = (e) => {
    const visibleRows = e.component.getVisibleRows();
    const newOrderIndex = visibleRows[e.toIndex].data.OrderNo;
    const oldOrderIndex = e.itemData.OrderNo;
    Updatereorder(newOrderIndex, oldOrderIndex);
  };

  const Updatereorder = async (newOrderIndex, oldOrderIndex) => {
    let scheduleHeaderID = props?.selectednewid.toString() || "";
    const requestBody = {
      scheduleHeaderID: scheduleHeaderID,
      OldOrderNo: oldOrderIndex,
      OrderNo: newOrderIndex,
    };
    const response = await putAPICall(
      "api/PlayLists/PutScheduleList",
      requestBody
    );

    console.log("Updatereorder", response?.data);
    fetchScheduleList();
  };
  const onRowInserting = (e) => {};
  const onDragEnd = (e) => {
    console.log(e);
  };

  const onAdd = async (e) => {
    const visibleRows = e.component.getVisibleRows();
    let OrderNo = 0;
    if (visibleRows?.length > 0)
      if (visibleRows?.length == e.toIndex) OrderNo = e.toIndex + 1;
      else OrderNo = e.toIndex;

    let scheduleHeaderID = props?.selectednewid.toString() || "";
    const requestBody = {
      scheduleHeaderID: scheduleHeaderID,
      OrderNo: OrderNo,
      MediaID: e.itemData.MediaID,
      MediaName: e.itemData.MediaName,
      MediaType: e.itemData.MediaType,
      MediaDuration: e.itemData.MediaDuration,
    };
    const response = await postAPICall(
      "api/PlayLists/PostScheduleList",
      requestBody
    );
    fetchScheduleList();
  };

  const DeleteRow = async (e) => {
    const requestBodydelete = {
      ScheduleDetailID: e.data.SchDtlID,
    };
    const response = await DeleteAPICall(
      "api/PlayLists/DeleteScheduleList",
      requestBodydelete
    );
    notifyMessage("Schedule ScheduleList Deleted..", "success");
    console.log(response?.data);
  };
  const openModal = () => {
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
  }; 
  const play = (e) => { 
    debugger
    if (e.rowIndex >= 0) {
      const Videolocation = s3url + e.data.MediaLocation;
      SetVideoPath(Videolocation)
      if (e.column.caption == 'Preview') { 
        openModal()
      }
    }
  };
  const handleKeyPress = (e) => { 
    if (e.event.key == "Enter") {
      const Videolocation = location;
      SetVideoPath(Videolocation)
      openModal()
    }
  }; 

  return (
    <div>
      <h1 hidden={true}>{props.selectednewid}</h1>
      <LoadPanel
        // shading={true}
        // shadingColor="rgba(0,0,0,0.4)"
        visible={loading}
        showIndicator={true}
        showPane={true}
      />
      <DataGrid
        id="ScheduleListGrid"
        dataSource={PlaylistSchList}
        onRowInserting={onRowInserting}
        onAdd={onAdd}
        onRowRemoving={DeleteRow}
        ref={gridRef}
        onKeyDown={handleKeyPress} 
        onCellClick={play} 
        hoverStateEnabled={true}
      >
        <Editing allowDeleting={true} />
        {/* <Column dataField="SchDtlID" visible={true} alignment="left" /> */}
        <Column
          dataField="OrderNo"
          width={100}
          alignment="left"
          visible={true}
        />
        <Column dataField="MediaName" visible={true} />
        {/* <Column dataField="MediaType" visible={true} /> */}
        <Column
          dataField="MediaDuration"
          caption="Duration"
          width={100}
          visible={true}
        />
         <Modal id="popup-video"
          isOpen={modalIsOpen}
          onRequestClose={closeModal}
          contentLabel="Video Modal"
        >
          <div>
            <video id="videoplay" controls tabIndex="0">
              <source src={location} type="video/mp4" />
              Your browser does not support the video tag.
            </video>
          </div>
        </Modal>
        <RowDragging
          allowReordering={true}
          onReorder={onReorder}
          showDragIcons={true}
          group="tasksGroup"
          onDragEnd={onDragEnd}
          onAdd={onAdd}
        />
        <Selection mode="single" />
        <ColumnChooser enabled={true} />
        <Summary>
          <TotalItem column="OrderNo" summaryType="count" />
          <TotalItem
            column="MediaDuration"
            summaryType="custom"
            displayFormat={TotalDuration}
          />
        </Summary>
      </DataGrid>
    </div>
  );
}
