import React, { useState } from 'react'
import Split from "react-split";
import UserCreation from '../../AdminControl/UserCreation/UserCreation.js'; 
import ChannelGrid from '../../AdminControl/ChannelGrid/ChannelGrid.js';
//import './UserToChannelMaster.css';
// import ChannelGrid from '../../AdminControl/ChannelGrid/ChannelGrid.js'; 
export default function UserToServerMaster() {
 const [ userid, setUserid]=useState();

  function onclicheck(userID) {
    setUserid(userID)
  }


  return (
    <div>
        <Split className="split"> 
        <div >  
        <UserCreation  onclickcheck={onclicheck} ColumnVisible={"0"} />
        </div>
        <div>
          <ChannelGrid  getuserid = {userid}  />
        </div> 
      </Split>
       </div>
  )
}
