import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.css";
import "./AdminHome.css";
import { NavLink, useNavigate } from "react-router-dom";
import CLSSLogo from "../../../Images/CLSSlogo.png";



export default function AdminHome() {
  let navigate = useNavigate();
  const SwithChannel = () => {
    navigate("/channelPopup");
  };
  return (
    <nav className="navbarContainer navbar-expand-lg ">
      <div id="fullContainer">
        <button
          className="navbar-toggler"
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
          <img
            id="clsslogo"
            src={CLSSLogo}
            class="rounded-circle"
            width="30"
            height="30"
            alt="Logo"
            loading="lazy"
          />
          <ul className="navbar-nav">
            <li class="nav-item">
              <NavLink id="num" to="/userMaster">
                User Creation
              </NavLink>
            </li>

            <li class="nav-item">
              <NavLink id="num" to="/channelMaster">
                Channel Creation
              </NavLink>
            </li>
            <li class="nav-item" id="schedulealignment">
              <NavLink id="num" to="/userservermap">
                User server map
              </NavLink>
            </li>
            <li class="nav-item" id="schedulealignment">
              <NavLink id="num" to="/config">
                Configuration
              </NavLink>
            </li>
          </ul>
        </div>
        <div className="userNimage">
          <i id="glyphicon" class="fa fa-th" onClick={SwithChannel}></i>

          <div className="userContainer">
            <div className="nav-item dropdown">
              <a
                className="nav-link"
                href="#"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                <img
                  src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQBLE2R35SV62Enw03QHS5AY-LUr6HOhmHvrA&usqp=CAU"
                  width="25"
                  height="25"
                  class="rounded-circle"
                />
              </a>

              <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                <a className="dropdown-item" href="/">
                  <i class="fa">&#xf08b;</i>
                  Logout
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
}
