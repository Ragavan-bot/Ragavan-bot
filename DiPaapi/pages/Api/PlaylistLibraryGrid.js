import React, { useState, useEffect } from "react";
import "devextreme/dist/css/dx.light.css";
import DataGrid, {
  SearchPanel,
  RowDragging,
  Editing,
  Summary,
  TotalItem,
  Selection,
} from "devextreme-react/data-grid";
import { postAPICall, s3url } from "../../api/api";
import "../Playlist/Playlist.css";
import { Column } from "devextreme-react/tree-list";
import Modal from "react-modal";
import { LoadPanel } from "devextreme-react";

export default function PlaylistLibraryGrid(props) {
  const [PlaylistLibrary, setPlaylistLibrary] = useState([]);
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
    props?.selectednewid != "" && fetchPlaylistLibrary();
    simulateLoading();
  }, [props]);

  const fetchPlaylistLibrary = async () => {
    let stringconversion = props?.selectednewid.toString() || "";
    const requestBody = {
      categoryId: stringconversion,
    };
    const response = await postAPICall(
      "api/PlayLists/ScheduleMediaList",
      requestBody
    );
    setPlaylistLibrary(response?.data);
  };
  const onAdd = (e) => {
    console.log(e);
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
      if (e.column.caption == "Preview") {
        openModal();
      }
    }
  };
  const handleKeyPress = (e) => {
    if (e.event.key == "Enter") {
      const Videolocation = location;
      SetVideoPath(Videolocation);
      openModal();
    }
  };

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
        id="PlaylistLibGrid"
        dataSource={PlaylistLibrary}
        allowColumnResizing={true}
        columnMinWidth={50}
        columnAutoWidth={true}
        onKeyDown={handleKeyPress}
        onCellClick={play}
        hoverStateEnabled={true}
      >
        <Selection mode="single" />
        <SearchPanel
          visible={true}
          highlightCaseSensitive={true}
          width={200}
          onAdd={onAdd}
        />
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
        <RowDragging
          allowReordering={true}
          showDragIcons={true}
          onAdd={onAdd}
          group="tasksGroup"
        />
        <Editing allowDeleting={true} />
        {/* <ColumnChooser enabled={true} /> */}
        <Summary>
          <TotalItem column="MediaName" summaryType="count" />
        </Summary>
      </DataGrid>
    </div>
  );
}
