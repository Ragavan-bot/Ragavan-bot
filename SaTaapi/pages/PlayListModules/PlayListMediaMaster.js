import React, { useEffect, useState } from 'react';
import { postAPICall } from '../../api/api';
import DataGrid, {
  Column,
  ColumnChooser
} from "devextreme-react/data-grid";
function PlayListMediaMaster() {
  const [mediamaster,setMediamaster]=useState([]);
  useEffect(()=>{
  GetMediaMasterGrid();
    },[])
    const GetMediaMasterGrid = async () => {debugger
      const response =await postAPICall("api/MediaMasters/GetMediaMaster");
      console.log(response?.data);
      setMediamaster(response?.data);
    }
  return (
    <div>
        <DataGrid  height={500} dataSource={mediamaster}>
            <ColumnChooser enabled={true} />
             <Column dataField='mediaId'/>
            <Column dataField='mediaNameTitle'/>
            <Column dataField='mediaTypeId'/>
            <Column dataField='albumName'/>
            </DataGrid>
    </div>
  )
}

export default PlayListMediaMaster;