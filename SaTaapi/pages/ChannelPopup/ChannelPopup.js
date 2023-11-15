import React, { useEffect } from 'react';
import { useState } from 'react';
import { Popup } from 'devextreme-react/popup';
import 'devextreme/dist/css/dx.common.css'; // Import the DevExtreme CSS for styling.
import 'devextreme/dist/css/dx.light.css';
import { Card, Row, Col, Container } from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.min.css';
import './ChannelPopup.css';
import { postAPICall } from '../../api/api';
import { useNavigate } from "react-router-dom";
function ChannelPopup() {
 const [playerData, setPlayerData] = useState([]);
 //const [getlocalstorageusername, setlocalstorageusername] = useState(localStorage.getItem("activeUser"));
 const [closeIcon,setCloseIcon]=useState(true);
 let navigate = useNavigate();
 useEffect(() => {
  if (localStorage.getItem("Loginusertype") === "EndUser") {
    setCloseIcon(false)
  }
fetchData()
//esc click popup  never close
const handleKeyPress = (e) => {
  if (e.key === 'Escape' && localStorage.getItem("activeUser") !== "Administration") {
    navigate('/login');
  }
  if(e.key ==='Escape' && localStorage.getItem("activeUser") === "Administration"){
    navigate('/channelPopup')
  }
};
window.addEventListener('keydown', handleKeyPress);

return () => {
  window.removeEventListener('keydown', handleKeyPress);
};
}, [navigate])
 var getid=localStorage.getItem("Getuserid")
const requestBody = {
  "userId": getid
}; 
const fetchData = async () => {
  debugger
  const  response  =  await postAPICall("api/Users/SPUserChannelPopup",requestBody)
  setPlayerData(response.data)
}  
// Initial Close Popup Navigate 
  const handleCloseClick = () => {
    navigate('/AdminHome');
  };
 
  // const ClickChannelName=(e)=>{debugger
  // console.log(e);
  // }
  const NavigatePage =(e)=>{debugger
    localStorage.setItem('Image',e.target.currentSrc);
    localStorage.setItem('CurrentChannelName', e.target.nextSibling.children.ChannelName.textContent);
    navigate('/scheduler')
  }
 return (
   <div>
   <div id='popup-container'>
     <Popup id='popup'
       visible={true}
       dragEnabled={false}
       showTitle={true}
       onHidden={handleCloseClick}
       title="Channel List"
       // titleTemplate={getlocalstorageusername}
       width={1250}
       height={600}
       showCloseButton={closeIcon}>
           {/* <div id='Username'> <span>UserName: </span>{getlocalstorageusername}</div> */}
          <Container>
           <Row className='Container' onClick={NavigatePage}>
               {playerData.map((playerData, k) => (
                   <Col key={k} xs={12} md={4} lg={3}>
                       <Card id='popupImgLogoNmae'>
                           <Card.Img  id='imgLogo' src={`data:image/jpeg;base64,${playerData.channellogo}`} alt="Image" />
                          {/* <p>{localStorage.setItem("",`data:image/jpeg;base64,${playerData.channelLogo}`)}</p> */}
                           <Card.Body >
                               <Card.Title id='ChannelName'>{playerData.channelName}</Card.Title>
                               <Card.Header hidden>{playerData.channelId}</Card.Header>
                           </Card.Body>
                       </Card>
                   </Col>
               ))}
           </Row>
       </Container>
     </Popup>
   </div>
   </div>
 );
}

export default ChannelPopup;
