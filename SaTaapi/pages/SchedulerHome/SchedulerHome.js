import React, { useEffect } from "react";
import { useState } from "react";
import './SchedeulerHome.css';
import "bootstrap/dist/css/bootstrap.css";
import { useNavigate,NavLink,Link } from "react-router-dom";
function SchedulerHome() {

  const [Admin, setAdmin] = useState(true);
  const [getlocalstorageuserName, setlocalstorageuserlogo] = useState(
    localStorage.getItem("activeUser")
  );
  let navigate = useNavigate();
  const [getimgLogo,setimgLogo]=useState(localStorage.getItem("Image")); 
  // const [getimgLogo,setimgLogo]=useState(''); 
  const[getChannelName,SetChannelName]=useState(localStorage.getItem("CurrentChannelName"));
  useEffect(() => {
    if (localStorage.getItem("Loginusertype") === "Administration") {
      setAdmin(false);
    }
  }, []);
  const SwithChannel = () => {
    navigate("/channelPopup");
  };


  return (
     <nav class="navbar navbar-expand-lg">
         <button id="ToglerButton"
        className="navbar-toggler bg-white"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>
      <div className="collapse navbar-collapse" id="navbarNav">
        <img  id="ChannelLogo"
          src={getimgLogo}
          class="brand-logo rounded"
          alt={getChannelName}
        />
           <ul className="navbar-nav">
          <li class="nav-item">
            <NavLink id="num" to="/TreeListGridSpliter">
            LIBRARY
            </NavLink>
          </li>
          <li class="nav-item">
            <NavLink id="num" to="/ClockLibraryMaster">
            CLOCKS
            </NavLink>
          </li>
          <li class="nav-item" id="schedulealignment">
            <NavLink id="num" to="/ScheduleMaster">
            SCHEDULE
            </NavLink>
          </li>
          <li class="nav-item" id="schedulealignment">
            <NavLink id="num" to="/PlayListMaster">
            PLAYLIST
            </NavLink>
          </li>
          <li class="nav-item" id="schedulealignment" >
            <NavLink id="num" to="/Report" hidden={Admin}>
            REPORT
            </NavLink>
          </li>
          <li class="nav-item" id="schedulealignment" >
            <NavLink id="num" to="/Administration" hidden={Admin}>
            ADMINISTRATION
            </NavLink>
          </li>
        </ul>
        </div>
        <div class="right-container">
          <i id="glyphicon" class='fa fa-th' onClick={SwithChannel}></i>
          <i class="fa fa-bell" placeholder="SwithChannel"></i>
          <div className="Username">{getlocalstorageuserName}</div>
          <div className="userContainer">
            <div className="nav-item dropdown">
              <a
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                <i class="fa fa-user-circle dropdown-content"></i>
              </a>
              <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                <a className="dropdown-item" href="/">
                  <i className="fa">&#xf08b;</i>
                  <span>Logout</span>
                </a>
              </div>
            </div>
          </div>
        </div>
        
      </nav> 
  );
}

export default SchedulerHome;
