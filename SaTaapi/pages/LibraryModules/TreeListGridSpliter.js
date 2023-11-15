import React from 'react'
import LibraryTreeList from './LibraryTreeList';
import LibraryMediaMasterGrid from './LibraryMediaMasterGrid';
import Split from "react-split-grid";
import './TreeListGridSpliter.css';
import SchedulerHome from '../SchedulerHome/SchedulerHome';
function TreeListGridSpliter() {
  return (
    <div>
    <SchedulerHome />
    <Split minSize={100} cursor="col-resize">
    {({ getGridProps, getGutterProps }) => (
      <div className="Split" {...getGridProps()}>
        <div id='spliterdata'>
         <LibraryTreeList />
        </div>
        <div className="SecondSplit" {...getGutterProps("column", 1)} />
        <div id='spliterdata'>
          <LibraryMediaMasterGrid />
        </div>
      </div>
    )}
  </Split>
  </div>
  )
}

export default TreeListGridSpliter;