import React from 'react'
import ChannelCreation from '../../AdminControl/ChannelCreation/ChannelCreation.js';
import UserCreation from '../../AdminControl/UserCreation/UserCreation.js';
import Split from "react-split";
import './ChannelToUserMaster.css';

export default function ServerToUserMaster() {
  return (
    <div>
        <Split className="split">
        
        <div id="SchTreeSplitter">
        <ChannelCreation  ColumnVisible={"0"}/>
        </div>
        <div id="SchScheduleHeader">
      <UserCreation ColumnVisible={"0"}/>
        </div>
      </Split>
       </div>
     
     
  )
}
