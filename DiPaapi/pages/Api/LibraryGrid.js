import React, { useState, useEffect } from "react";
import axios from "axios";
import "devextreme/dist/css/dx.light.css";
import DataGrid, {
  Summary,
  TotalItem,
  Selection,
} from "devextreme-react/data-grid";
import {
  BAPassword,
  BAUsername,
  axiosDTVbasicAuthForm,
  DeleteAPICall,
  axiosapibaseurl,
  notifyMessage,
  postAPICall,
  s3url,
} from "../../api/api";
import { Column, Editing } from "devextreme-react/tree-list";
//styleSheet
import "../StyleSheet/StyleSheet.css";
import "../Library/Library.css";
import Modal from "react-modal";
import { LoadPanel } from "devextreme-react";

export default function LibraryGrid(props) {

  const [LibraryGridData, setLibraryGridData] = useState([]);
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
//useeffect changes
  useEffect(() => {
    props?.selectednewid !== "" && fetchLibraryGridData();
    simulateLoading();
  },[props]);
  
  const fetchLibraryGridData = async () => {
    let stringconversion = props?.selectednewid.toString() || "";
    const requestBody = {
      categoryId: stringconversion,
    };
    const response = await postAPICall(
      "api/Library/GetLibraryData",
      requestBody
    );
    setLibraryGridData(response?.data);
  };
  function calculateSelectedRow(options) {
    options.totalValue += options.value.Duration;
  }

  const onRowRemovedLibraryMedia = async (e) => {
    debugger;
    const requestBody = {
      mediaID: e.data.MediaID,
    };
    const response = await DeleteAPICall(
      "api/Library/DeleteLibraryMedia",
      requestBody
    );
    notifyMessage(response.data[0].Result, "success");
  };
  const handleUpload = async (e) => {
    debugger;
    console.log(e.target.files);
    if (props?.selectednewid !== "" && e.target.files.length > 0) {
      console.log("selectedFiles", e.target.files);
      const formData = new FormData();
      for (let i = 0; i < e.target.files.length; i++) {
        formData.append("file", e.target.files[0]);
        formData.append("CategoryID", props?.selectednewid);
      }
      try {
        let endpointURL = "api/Library/PostLibraryDataFiles";
        const response = await axios.post(
          `${axiosapibaseurl}${endpointURL}`,
          formData,
          {
            auth: { username: BAUsername, password: BAPassword },
            headers: { axiosDTVbasicAuthForm },
            // onUploadProgress,
          }
        );
        console.log("result", response);
        notifyMessage(response.data, "success");
        fetchLibraryGridData();
      } catch (error) {}
    } else {
      notifyMessage("Please Select the file and Category ID", "warning");
    }
  };
  //Video Preview
  const openModal = () => {
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
  };
  const play = (e) => {
    if (e.rowIndex >= 0) {
      const Videolocation = s3url + e.data.MediaLocation;
      SetVideoPath(Videolocation);
      if (e.column.caption === "Preview") {
        openModal();
      }
    }
  };
  const handleKeyPress = (e) => {
    if (e.event.key === "Enter") {
      const Videolocation = location;
      SetVideoPath(Videolocation);
      openModal();
    }
  };
  return (
    <div>
      <div>
        {/* <p hidden>{props.selectednewid}</p> */}
        <p>Name: {props.selectednewName}</p>
        <form id="FileUpload">
          <label id="first">
            <div id="wrapper">
              <input id="choose-file" type="file" onChange={handleUpload} />
              <i id="uploadIcon" class="fa">
                &#xf093;
              </i>
            </div>
          </label>
        </form>
      </div>
      <LoadPanel
        // shading={true}
        // shadingColor="rgba(0,0,0,0.4)"
        visible={loading}
        showIndicator={true}
        showPane={true}
      />
      <DataGrid
        id="LibraryGrid"
        dataSource={LibraryGridData}
        onRowRemoving={onRowRemovedLibraryMedia}
        hoverStateEnabled={true}
        allowColumnResizing={true}
        onKeyDown={handleKeyPress}
        onCellClick={play}
      >
        <Selection mode="single" />
        <Column dataField="MediaName" visible={true} />
        <Column
          dataField="MediaDuration"
          caption="Duration"
          width={100}
          visible={true}
        />
        <Column
          dataField="MediaLocation"
          caption="Location"
          visible={true}
          width={true}
        />
        <Column dataField="MediaSizeMB" visible={true} />
        <Column dataField="CreatedOn" width={150} visible={true} />
        <Column
          caption="Preview"
          width={100}
          allowFiltering={false}
          allowSorting={false}
          cellRender={({ data }) => (
            <button id="playicon">
              <span role="img" aria-label="Play video">
                ▶️
              </span>
            </button>
          )}
        />
        <Modal
          id="popup-video"
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
        <Editing allowDeleting={true} />
        <Summary calculateCustomSummary={calculateSelectedRow}>
          <TotalItem column="MediaName" summaryType="count" />
        </Summary>
      </DataGrid>
    </div>
  );
}
