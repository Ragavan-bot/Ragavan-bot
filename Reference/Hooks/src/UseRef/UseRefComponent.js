import React, { useEffect, useRef } from 'react'
const colors =['#ff0000','#00ff00','#0000ff'];
function UseRefComponent() {
    const bodycolor=useRef(null);

    useEffect(()=>{
 console.log(bodycolor);
 console.log(bodycolor.current);
},[])
   const styleBodyColor=()=>{

     let randomcolor=Math.floor(Math.random() * colors.length);
     bodycolor.current.style.color=colors[randomcolor];

    }
  return (
    <div>
        <button onClick={()=>{styleBodyColor()}}>Click Me</button>
        <p ref={bodycolor}>UseRefComponent</p>
    </div>
  )
}

export default UseRefComponent;