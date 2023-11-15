import React, { useContext } from 'react'
import { userContext } from './UseContextparent';

function UseContextChildTwo() {
    const {user,name} = useContext(userContext);
    const [stateuser, setStateuser] = user;
    const [statename, setStatename] = name;
    
    const Changer=()=>{
        let names= "Karthik";
        setStatename(names)
    }
  return (
    <div>
   <p>{`Welcome useContext ${stateuser}`}</p>
   <button onClick={Changer}>Click</button>
   <p>Global SetState</p>
   <p>{`hello ${statename}`}</p>
   </div>
  )
}

export default UseContextChildTwo;