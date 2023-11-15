import React, { useState, useEffect, useRef } from 'react'
import { DeleteAPICall, notifyMessage, postAPICall, putAPICall } from '../../../api/api.js';
import './ChannelCreation.css';
import DataGrid, {
  Selection,
  Column,
  ColumnChooser,
  Popup, Form,
  Editing,
  FormItem,CheckBox ,
  TableSelection,
  Lookup,
  SearchPanel
} from "devextreme-react/data-grid";
import FileUploader from 'devextreme-react/file-uploader';
export default function ChannalCreation(props) {
  const [ChannelCreation, setChannelCreation] = useState([]);
  const [imageData, setImageData] = useState(null);
  const logoupdate = useRef(null);
  const [UserChannelResponse, setUserChannelResponse] = useState();

  const [ColumnVisible, setColumnVisible] = useState(true)
  const [UseridCheckBox, setUseridCheckBox] = useState();

  logoupdate.current = imageData
  useEffect(() => {
    props?.getUserId || fetchChannelCreation();
  }, [props]);

  const fetchChannelCreation = async () => {
    const requestBody = {
      "": ""
    };
    const response = await postAPICall("api/Channels/GetChannel", requestBody);
    console.log("result", response?.data)
    setChannelCreation(response?.data);
    ChannelColumnVisible();
    //selectionFilter();
    GetUserId();

    //setImageData(response.data[0].channelLogo);
  }
  const ChannelColumnVisible = () => {
 
    props.ColumnVisible == 1 ? setColumnVisible(true) : setColumnVisible(false);
  }

  const AddChannel = async (e) => {
    try {
      let userNew = e.data;
      let channelName = userNew.channelName;
      let channelCode = userNew.channelCode;
      let channelDescription = userNew.channelDescription;
      let channelLogo = imageData;
      let companyId = userNew.companyId;
      let activeStatus = userNew.activeStatus ? 1 : 0;
      let licenseCode = userNew.licenseCode;
      const requestBody = {
        channelName: channelName,
        channelCode: channelCode,
        channelDescription: channelDescription,
        channelLogo: channelLogo,
        companyId: companyId,
        activeStatus: activeStatus,
        licenseCode: licenseCode,
      };
      const response = await postAPICall(
        "api/Channels/PostChannelCreation",
        requestBody
      );
      notifyMessage('Channel Added successfully...', "success");
      setImageData(null)
      fetchChannelCreation()
    }
    catch (error) {
      notifyMessage('Channel Added Failed...', "warning");
    }
  };
  const handleFileUpload = (e) => {
    
    const file = e.value[0];
    if (file) {
      // Read the file as a data URL
      const reader = new FileReader();
      reader.onload = (event) => {
        let binaryData = event.target.result.replace('data:image/png;base64,', '');
        setImageData(binaryData);
      };
      reader.readAsDataURL(file);

    }
  }
  //Display Images
  function cellRender(data) {
    return <img src={`data:image/jpeg;base64, ${data.value}`} alt="Image" style={{ maxWidth: '60px', maxHeight: '30px' }} />
  }
  //Update
  const UpdateChannel = async (e) => {
    
    try {
      let ChannelNew = e.newData;
      let Channeold = e.oldData;
      let channelId = Channeold.channelId;
      let channelName = ChannelNew.channelName ? ChannelNew.channelName : Channeold.channelName;
      let channelCode = ChannelNew.channelCode ? ChannelNew.channelCode : Channeold.channelCode;
      let channelDescription = ChannelNew.channelDescription ? ChannelNew.channelDescription : Channeold.channelDescription;
      let channelLogo = imageData; //ChannelNew.channelLogo ? ChannelNew.channelLogo :Channeold.channelLogo;
      let companyId = ChannelNew.companyId ? ChannelNew.companyId : Channeold.companyId;
      let activeStatus = ChannelNew.activeStatus ? ChannelNew.activeStatus : Channeold.activeStatus ? 1 : 0;
      let licenseCode = ChannelNew.licenseCode ? ChannelNew.licenseCode : Channeold.licenseCode;
      const requestBody = {
        channelId: channelId,
        channelName: channelName,
        channelCode: channelCode,
        channelDescription: channelDescription,
        channelLogo: channelLogo,
        companyId: companyId,
        activeStatus: activeStatus,
        licenseCode: licenseCode,
      };
      const response = await putAPICall(

        "api/Channels/PutChannelCreation",
        requestBody
      );
      notifyMessage('Channel Updated successfully...', "success");
      fetchChannelCreation()
    }
    catch (error) {
      notifyMessage('Channel Updated Failed...', "warning");
    }
  };
  //Delete
  const DeleteChannel = async (e) => {
    
    try {
      let DeleteChannel = e.data;
      let channelId = DeleteChannel.channelId;
      const requestBody = {
        channelId: channelId,
      };
      const response = await DeleteAPICall(
        "api/Channels/DeleteChannelCreation",
        requestBody
      );
      notifyMessage('Channel Deleted successfully...', "success");
    }
    catch (error) {
      notifyMessage('Channel Deleted Failed...', "warning");
    }
  }


  const GetUserId = async () => {
   
    try {
      let userId = props.getuserid;
      const requestBody = {
        userId: userId,

      };
      console.log("getuserid", userId);
      const response = await postAPICall(
        "api/UserChannelMaps/GetUserServerMap",
        requestBody
      );
      setUserChannelResponse(response?.data)
      debugger
     console.log("RESPONSE DATA", response?.data);
     setUseridCheckBox(response.data[0].userId);

    } catch (error) {

    }
  }
  
 
  return (
    <div>
      <DataGrid
        id="ScheduleListGrid"
        dataSource={ChannelCreation}
        onRowInserting={AddChannel}
        onRowUpdating={UpdateChannel}
        onRowRemoved={DeleteChannel}  
      >
           
        <Editing mode='popup' allowAdding={true} allowDeleting={ColumnVisible} allowUpdating={ColumnVisible} >
          <Popup title="Channels" showTitle={true} width={700} height={525} />
        </Editing>
        <ColumnChooser enabled={true} />
        <Selection mode="single" deferred={true} />

        <Column dataField="channelId" allowEditing={false} visible={ColumnVisible} />
        <Column dataField="channelName">
        <CheckBox />
        </Column>
        <Column dataField="channelCode" />
        <Column dataField="channelDescription" visible={ColumnVisible} />
        <Column dataField="channelLogo" ref={logoupdate} visible={ColumnVisible}
          width={70}
          allowSorting={false}
          cellRender={cellRender}>
          <FormItem>
            <FileUploader
              uploadMode="useForm"
              name="file"
              accept="image/*,.pdf"
              onValueChanged={handleFileUpload}
            />
          </FormItem>
        </Column>

        <Column dataField="companyId" visible={ColumnVisible} />
        <Column dataField="activeStatus" dataType="boolean" visible={ColumnVisible} />
        <Column dataField="createdOn" visible={false} />
        <Column dataField="changedOn" visible={false} />
        <Column dataField="createdBy" visible={false} />
        <Column dataField="changedBy" visible={false} />
        <Column dataField="licenseCode" visible={false} />
      </DataGrid>
    </div>
  )
}
