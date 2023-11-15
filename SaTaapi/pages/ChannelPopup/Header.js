import React from 'react'
import { useNavigate } from "react-router-dom";
function Header() {
    let navigate = useNavigate();
    const OpenPopup=()=>{
navigate('/channelPopup');
    }
  return (
    <div>
        <h1>Header</h1>
<button onClick={OpenPopup} className='btn btn-primary'>Channel List</button>
    </div>

  )
}

export default Header;