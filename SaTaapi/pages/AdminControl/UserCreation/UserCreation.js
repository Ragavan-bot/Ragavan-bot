import React, {useState,useEffect} from 'react'
import  { companyID, DeleteAPICall, notifyMessage, postAPICall, putAPICall, UserLogo } from '../../../api/api.js';
import DataGrid, {
  Selection,
  Column,
  ColumnChooser,
Lookup,
Popup,
  Editing,
  FormItem,
} from "devextreme-react/data-grid"; 
import FileUploader from 'devextreme-react/file-uploader';


export default function ChannelCreation(props) {
  const [UserCreation, setUserCreation] = useState([]);
  const [imageData, setImageData] = useState(null);
  // const [imageData, setImageData] = useState(UserLogo);
  const [userTypeData, setUserTypeData] = useState([]);
 

const [ColumnVisible, setColumnVisible]=useState(true)

 


  const fetchUserMaster = async () => {
 
    const requestBody = {
      "":""
    };

    const response = await postAPICall(
      "api/Users/GetUserMaster",
      requestBody
    );
    console.log("result",response?.data)
    
    setUserCreation(response?.data);
   
    fetchUserType();
    UserColumnVisible();
  } 
 
const UserColumnVisible=()=>{
   
  props.ColumnVisible==1?setColumnVisible(true):setColumnVisible(false);
 
}


useEffect(() => { 
   fetchUserMaster();
}, []);
 

const fetchUserType = async () => {
   
  try {
    const requestBody = {
      "": "",
    };
    const response = await postAPICall(
      "api/Users/GetUserTypeMaster",
      requestBody
    );
    console.log(response?.data);
    setUserTypeData(response?.data);
  } catch (error) {
    console.error("Error fetching data:", error);
  }
};

const userAdd = async (e) => {
  
  try {
    let userNew = e.data;


      let userName = userNew.userName;
      let password = userNew.password;
      let userImage = imageData;
      let userTypeId = userNew.userTypeId;
      let companyId = userNew.companyId;
      let activeStatus = userNew.activeStatus? 1 : 0;
    
      const requestBody = {
        userName: userName,
        password: password,
        userImage: userImage,
        userTypeId: userTypeId,
        companyId: companyId,
        activeStatus: activeStatus,
      };
      const response = await postAPICall(
        "api/Users/PostUserCreation",
        requestBody
      );
      notifyMessage('User Added successfully...', "success");
  } catch (error) {
    notifyMessage('User Added Failed...', "warning");
  }
  //setImageData(UserLogo);
  fetchUserMaster();
};

const userUpdate = async (e) => {
 
  try {
    const userOld = e.oldData;
    const userNew = e.newData;

  let userId = userOld.userId;
  let userName = userNew.userName?userNew.userName:userOld.userName;
  let password = userNew.password?userNew.password:userOld.password;
  let userImage = imageData?imageData:null;
  let userTypeId = userNew.userTypeId?userNew.userTypeId:userOld.userTypeId;
  let companyId = userNew.companyId?userNew.companyId:userOld.companyId;
  let activeStatus = userNew.activeStatus?userNew.activeStatus:userOld.activeStatus;

  const requestBody = {
    userId: userId,
    userName: userName,
    password: password,
    userImage: userImage,
    userTypeId: userTypeId,
    companyId: companyId,
    activeStatus: activeStatus,
  };
  const response = await putAPICall(
    "api/Users/PutUserCreation",
    requestBody
  );
  notifyMessage('User Updated Successfully...', "success");
  } catch (error) {
    notifyMessage('User Updated Failed...', "warning");
  }
  
  fetchUserMaster();
}; 
const userDelete = async (e) => {
   
  try {
    let userId = e.data.userId;
    const requestBody = {
      userId: userId,
    };
    const response = await DeleteAPICall(
      "api/Users/DeleteUserCreation",
      requestBody
    );
    notifyMessage('User Deleted Successfully', "success");
  } catch (error) {
    notifyMessage('User Deleted Failed...', "warning");
  } 
  fetchUserMaster();
}; 
//Display Images
function cellRender(data) { 
   
  return <img src={`data:image/jpeg;base64, ${data.value}`} alt="Image" style={{ maxWidth: '60px', maxHeight: '30px' }} />
} 
const handleFileUpload = (e) => { 
 
  const file = e.value[0];
  if (file) {
    // Read the file as a data URL
    const reader = new FileReader();
    reader.onload = (event) => {
      let binaryData = event.target.result.replace('data:image/png;base64,', ''); // This is the image in data URL format
      setImageData(binaryData);
    };
    reader.readAsDataURL(file);

  } 
}

const onSelectionChanged=async (e)=>{
 
  if( props.ColumnVisible!=1){
    props.onclickcheck(e.selectedRowsData[0].userId)
  }
  
}
return (
    <div>
 
    <DataGrid
        dataSource={UserCreation}
        
        onRowInserting={userAdd}
        onRowRemoving={userDelete}
        onRowUpdating={userUpdate}
        onSelectionChanged={onSelectionChanged}
      >
        <ColumnChooser enabled={true} />
        <Selection mode="single"/>
      

        <Editing mode='popup'  allowAdding={true} allowDeleting={ColumnVisible} allowUpdating={ColumnVisible}>
        <Popup title="User" showCloseButton={true} showTitle={true} width={800} height={600} />
        </Editing>
        <ColumnChooser enabled={true} />
        
        <Column dataField="userId" allowEditing={false} visible={ColumnVisible} />
        <Column dataField="userName"  />
        <Column dataField="password" visible={ColumnVisible}/>
        <Column dataField="userImage" visible={ColumnVisible} 
         cellRender={cellRender}   
         > 
          <FormItem>
          <FileUploader 
                  uploadMode="useForm"
                  accept="image/*"
                  multiple={false} 
                  onValueChanged={handleFileUpload} 
                />
          </FormItem>
         </Column> 
        <Column dataField="userTypeId" caption="User Type">
          <Lookup
            dataSource={userTypeData}
            displayExpr="userType"
            valueExpr="userTypeId"
          />
        </Column>

        <Column dataField="activeStatus" dataType='boolean' visible={ColumnVisible}/>
        <Column dataField="companyId"  visible={ColumnVisible}/>
      </DataGrid>
    </div>
  )
}
