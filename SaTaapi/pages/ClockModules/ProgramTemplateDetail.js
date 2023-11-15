import React from 'react'
import { NavLink } from 'react-router-dom';
import './ClockLibraryMaster.css';
import SchedulerHome from '../SchedulerHome/SchedulerHome';
function ProgramTemplateDetail() {
  return (
    <div>
      <SchedulerHome />
        <div id='navbar'>
            <NavLink id="num" to="/">
             MODULE1
            </NavLink>
            <NavLink id="num" to="/">
            MODULE2
            </NavLink>
            <NavLink id="num" to="/">
            MODULE3
            </NavLink>
            </div>
    </div>
  )
}

export default ProgramTemplateDetail;