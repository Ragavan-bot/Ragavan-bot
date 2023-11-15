import React from 'react';
import AdminHome from '../AdminControl/AdminHome/AdminHome.js';
import UserCreation from '../AdminControl/UserCreation/UserCreation.js';

export default function UserMaster() {
  return (
    <div>
        <AdminHome/>  
        <UserCreation  ColumnVisible={"1"}/>
      </div>
  )
}
