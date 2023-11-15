 import React, { useEffect, useState } from 'react'
import DataGrid,{Column, Selection} from 'devextreme-react/data-grid';
import { postAPICall } from '../../../api/api';

 
 export default function ChannelGrid(props ) {
    const [ChannelGrid,setChannelGrid]=useState([]);
    const [UseridCheckBox, setUseridCheckBox] = useState('');
useEffect(()=>{
    props?.getuserid && GetChannelGrid();
  },[props])

  const requestBody = {
    userId:props.getuserid
  };
  const GetChannelGrid = async () => {debugger
    const response =await postAPICall("api/Users/SPUsertoChannel",requestBody);
    console.log(response?.data);
    setChannelGrid(response?.data); 
    GetUserId();

  }
  const selectionFilter =['userId','=',UseridCheckBox];

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
      debugger
     console.log("RESPONSE DATA", response?.data);
     setUseridCheckBox(response.data[0].userId);

    } catch (error) {

    }
  }
const selectionFilter1=(e)=>{
  debugger

}

   return (
    <div>
    <h1>{props.getuserid}</h1>
    <DataGrid  height={500} dataSource={ChannelGrid}   
      selectionFilter={selectionFilter} 
      >
        <Column dataField='channelId'/>
        <Column dataField='channelName'/>
        <Column dataField='userId'/>
        <Selection mode="multiple" deferred={true} />
    </DataGrid>
    </div>
   )
 }
 