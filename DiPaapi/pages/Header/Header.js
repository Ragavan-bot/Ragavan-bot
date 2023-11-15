// import {Navbar,Nav} from 'react-bootstrap'
import CLSSlogo from "../Images/CLSSlogo.png";
import { NavLink, Link } from "react-router-dom";
import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.css";
import "./Header.css";
import { ReactComponent as Sun } from  "./Sun.svg";
import { ReactComponent as Moon } from  "./Moon.svg";
 

export default function Header() {
  const [isNavOpen, setIsNavOpen] = useState(false);
  const [Admin, setAdmin] = useState(true);
  const [toggleThemeValue, setToggleTheme] = useState(false);
  const [getlocalstorageusername, setlocalstorageusername] = useState(
    localStorage.getItem("logginuser")
  );

  useEffect(() => {
    localStorage.getItem("Toggle")=='true'?setToggleTheme(true):setToggleTheme(false);
    if (localStorage.getItem("Loginusertype") == "Administration") {
      setAdmin(false);
    }
  });
  const toggleNav = () => {
    setIsNavOpen(!isNavOpen);
  };
  const setDarkMode = () => {
    localStorage.setItem("Toggle", true);
    setToggleTheme(true);
    document.querySelector("body").setAttribute("data-theme", "dark");
  };
  const setLightMode = () => {
    localStorage.setItem("Toggle", false);
    setToggleTheme(false);
    document.querySelector("body").setAttribute("data-theme", "light");
  };
  const toggleThemeChange = (e) => {
    e.target.checked ? setDarkMode() : setLightMode();
  }; 

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
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
          src={CLSSlogo}
          class="rounded-circle"
          width="30"
          height="30"
          alt="Logo"
          loading="lazy"
        />
        <ul className="navbar-nav">
          <li class="nav-item"  hidden={Admin}>
            <NavLink id="num" to="/master">
              Master
            </NavLink>
          </li>

          <li class="nav-item">
            <NavLink id="num" to="/library">
              Library
            </NavLink>
          </li>
          <li class="nav-item">
            <NavLink id="num" to="/playlist">
              Playlist
            </NavLink>
          </li>
          <li class="nav-item" id="schedulealignment">
            <NavLink id="num" to="/schedule">
              Schedule
            </NavLink>
          </li>
        </ul>
      </div>
       {/* Theme Switch Toggle */}
       <div  className="themeSwitch" >
        <div className="dark_mode">
          <input
            className="dark_mode_input"
            type="checkbox"
            id="darkmode-toggle"
            checked={toggleThemeValue}
            onChange={toggleThemeChange}
          />
          <label  className="dark_mode_label" for="darkmode-toggle">
            <Sun  />
            <Moon />
          </label>
        </div>
      </div>
      <div className="userNimage">
        <div className="userName text-dark">{getlocalstorageusername}</div>

        <div className="userContainer">
          <div className="nav-item dropdown">
            <a
              className="nav-link dropdown-toggle"
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
              <a className="dropdown-item"  href="/">
                <i class="fa">&#xf08b;</i>
                Logout
              </a>
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
}

