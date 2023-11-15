import React, { useEffect, useState } from 'react';
import { NavLink } from 'react-router-dom';
import './LibraryMediaMasterGrid.css';
import DataGrid, {
  Column,
  ColumnChooser,
  Editing,
  Lookup,
} from "devextreme-react/data-grid";
import SchedulerHome from '../SchedulerHome/SchedulerHome';
import { postAPICall } from '../../api/api';
function LibraryMediaMasterGrid() {
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
      <SchedulerHome />
      <div id='Navbars'>
            <NavLink id="Gridnav" to="/">
             MODULE1
            </NavLink>
            <NavLink id="Gridnav" to="/">
            MODULE2
            </NavLink>
            <NavLink id="Gridnav" to="/">
            MODULE3
            </NavLink>
        
            <NavLink id="Gridnav" to="/">
            MODULE4
            </NavLink>
            </div>
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
export default LibraryMediaMasterGrid;