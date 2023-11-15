import React from 'react'
import PlayList from './PlayList';
import Split from "react-split-grid";
import SchedulerHome from '../SchedulerHome/SchedulerHome';
import PlayListMediaMaster from './PlayListMediaMaster';
import './PlayListMaster.css';
function PlayListMaster() {
  return (
    <div>
       <SchedulerHome />
       <Split minSize={100} cursor="col-resize">
    {({ getGridProps, getGutterProps }) => (
      <div className="PlayListMasterSpliter" {...getGridProps()}>
        <div id='spliterdata'>
        <PlayList />
        </div>
        <div className="LeftSideSplit" {...getGutterProps("column", 1)} />
        <div id='spliterdata'>
         <PlayListMediaMaster />
        </div>
      </div>
    )}
  </Split>
        
    </div>
  )
}

export default PlayListMaster;