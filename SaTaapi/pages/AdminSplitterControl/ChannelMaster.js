import React from 'react'
import AdminHome from '../AdminControl/AdminHome/AdminHome'
import ChannelCreation from '../AdminControl/ChannelCreation/ChannelCreation'

export default function ChannelMaster() {
  return (
    <div> 
        <AdminHome/>
        <ChannelCreation ColumnVisible={"1"}/>
    </div>
  )
}
