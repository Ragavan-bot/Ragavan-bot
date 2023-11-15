import React, { useEffect, useState } from 'react'

function UseEffectComponent() {
    const [count,setcount] =useState(0);
    const [msg,setMsg]=useState('Initial message');

useEffect(()=>{
  console.log("Message");
console.log(msg);
console.log("Count Updated to",count);
    },[msg,count]);//which state u passed its only worked Array dependencies[]
  return (
    <div>
          <button onClick={()=>{setcount(count +1)}}>Increase Count</button>
          <p>{count}</p>
          <button onClick={()=>{setMsg("Msg Updated")}}>Msg Click</button>
          {/* <button onClick={Clcikcer}>{msg1} hello</button> */}
          <p>{msg}</p>
    </div>
  
  )
}

export default UseEffectComponent;