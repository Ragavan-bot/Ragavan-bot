import React, { useContext } from 'react'
import { userContext } from './UseContextparent.js'

export default function UseContextChild() {
  const { user, name } = useContext(userContext);
  const [stateuser, setStateuser] = user;//usestate
  const [statename, setStatename] = name;

  
  return (
  <div>
    {`Welcome to learn ${stateuser}`}
    {`Welcome ${statename}`}
    </div>)
}

