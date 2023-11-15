import React, { useState } from "react";
import "../Userservermap/Userservermap.css";
import { Button } from "react-bootstrap";

import "bootstrap/dist/css/bootstrap.css";
import AdminHome from "../../AdminControl/AdminHome/AdminHome.js";
import UserToChannelMaster from "../../AdminSplitterControl/UserToChannelMaster/UserToChannelMaster.js";
import  ChannelToUserMaster from "../../AdminSplitterControl/ChannelToUserMaster/ChannelToUserMaster.js";

export default function Userservermap() {
  const [value, setValue] = useState("User");

  const UserClick = (getValue) => {
    setValue(getValue);
  };
  return (
    <div>
      <AdminHome />
      <div>
        <div id="userservertab">
          <div id="TabControl">
            <Button
              id="tab"
              className={value == "User" ? "active btn btn-dark" : "inactive"}
              icon=''
              onClick={() => UserClick("User")}
            >
              <span><i class="glyphicon glyphicon-user"></i> User To Server</span>
            </Button>
            <Button
              id="tab"
              className={value == "Server" ? "active btn btn-dark" : "inactive"}
             
              onClick={() => UserClick("Server")}
            >
              <span><i class='glyphicon glyphicon-blackboard'></i> Server To User</span>
            </Button>
          </div>
        </div>
        {value == "User" ? <UserToChannelMaster /> : ""}
        {value == "Server" ? <ChannelToUserMaster /> : ""}
      </div>
    </div>
  );
}
