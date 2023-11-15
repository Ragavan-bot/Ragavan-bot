import React, { useState, useEffect, useRef } from "react";
import Scheduler, { AppointmentDragging } from "devextreme-react/scheduler";
import {
  DeleteAPICall,
  notifyMessage,
  postAPICall,
  putAPICall,
} from "../../api/api";
import "../Schedule/Schedule.css";
import Button from "devextreme-react/button";
import { DateRangeBox } from "devextreme-react/date-range-box";
import { CheckBox } from "devextreme-react/check-box";
import { useNavigate } from "react-router-dom";
import { LoadPanel } from "devextreme-react";

const currentDate = new Date();
const views = ["week", "day", "month"];

export default function Schedulectrl(props) {
  const [scheduleData, setData] = useState([]);
  const [CStartDate, setcopyStartDate] = useState();
  const [CEndDate, setcopyEndDate] = useState();
  const [AStartDate, setActiveStartDate] = useState();
  const [AEndDate, setActiveEndDate] = useState();
  const gridRef = useRef(null);
  const daterangeref = useRef(null);
  //check box
  const [selItem, setSelItem] = useState(null);
  let navigate = useNavigate();
   //loader
   const [loading, setLoading] = useState(false);
   // Function to show the loading panel
 const showLoadingPanel = () => {
   setLoading(true);
 };
 // Function to hide the loading panel
 const hideLoadingPanel = () => {
   setLoading(false);
 };
 // Simulate a loading operation
const simulateLoading = () => {
   showLoadingPanel();
   // Simulate some asynchronous operation (e.g., API call, setTimeout)
      setTimeout(() => {
     hideLoadingPanel();
   }); // Simulate loading for 3 seconds
 };

  useEffect(() => { 
    props?.selectednewid && fetchScheduleData();
    simulateLoading();
  }, [props]);

  const fetchScheduleData = async () => {
    simulateLoading();
    //Select Visible Schedule Name
    document.getElementById("schName").style.visibility = "visible";

    console.log("this.props selectednewid", props?.selectednewid);
    let stringconversion =
      props?.selectednewid != "" ? props?.selectednewid.toString() : "";
    const requestBody = {
      ScheduleLibraryID: stringconversion,
    };
    let response = await postAPICall(
      "api/Schedule/GetLoadSchedule",
      requestBody
    );
    if (response?.status == 200) {
      setData(response?.data);
    } else {
      alert("no data found");
      setData([]);
    }
  };
  const onAppointmentAdd = async (e) => {
    let schLibID =
      props?.selectednewid != "" ? props?.selectednewid.toString() : "";
    if (schLibID != "") {
      const requestBody = {
        startDate: e.itemData.StartDate,
        endDate: e.itemData.EndDate,
        subject: e.itemData.Schedulename,
        scheduleHeaderID: e.itemData.ScheduleHdrId,
        scheduleLibraryID: schLibID,
      };
      const response = await postAPICall(
        "api/Schedule/PostSaveSchedule",
        requestBody
      );
      fetchScheduleData();
    } else {
      notifyMessage("Please Select LibraryId", "warning");
    }
  };
  const onAppointmentDelete = async (e) => {
    const requestBody = {
      id: e.appointmentData.ID,
    };
    const response = await DeleteAPICall(
      "api/Schedule/DeleteSchedule",
      requestBody
    );
    fetchScheduleData();
  };

  //PUT api Call to Change Your Appointment SDate,EDate
  const onupdateAppointment = async (e) => {
    console.log("Onappoinementplace", e);
    const requestBody = {
      id: e.appointmentData.ID,
      startDate: e.appointmentData.StartDate,
      endDate: e.appointmentData.EndDate,
      subject: e.appointmentData.Subject,
    };
    //Make a PUT request to update the appointment place
    const response = await putAPICall("api/Schedule/PutSchedule", requestBody);
    fetchScheduleData();
  };

  //popup disabled
  const appoinmentFormOpening = (e) => {
    e.cancel = true;
  };

  //Copy SDate & EDate
  const CopySDatetoEData = async () => {
    if (gridRef.current) {
      const schedulerInstance = gridRef.current.instance;
      const viewStartDate = schedulerInstance.getStartViewDate();
      const viewEndDate = schedulerInstance.getEndViewDate();
      console.log("View Start Date:", viewStartDate);
      console.log("View End Date:", viewEndDate);
    }
    if (daterangeref.current) {
      const datarangeinstance = daterangeref.current.instance;
      console.log("Copy Start Date:", datarangeinstance[0]);
      console.log("Copy End Date:", datarangeinstance[1]);
    }

    const today = new Date();
    //✅ Get the first day of the current week (Sunday)
    const firstDayinweek = new Date(
      today.setDate(today.getDate() - today.getDay())
    );
    //✅ Get the last day of the current week (Saturday)
    const lastDayinweek = new Date(
      today.setDate(today.getDate() - today.getDay() + 6)
    );
    //Check Change Date or Not Change Date
    if (AStartDate != undefined) {
      var sdate = AStartDate;
      var edate = AEndDate;
      sdate = AStartDate;
      edate = AEndDate;
    } else if (AStartDate == undefined) {
      var sdate = firstDayinweek;
      var edate = lastDayinweek;
      sdate = firstDayinweek;
      edate = lastDayinweek;
    }
    let stringconversion =
      props?.selectednewid != "" ? props?.selectednewid.toString() : "";
    if (
      stringconversion != "" &&
      CStartDate != undefined &&
      CEndDate != undefined
    ) {
      //validation msg
      const requestBody = {
        scheduleLibraryID: stringconversion,
        copyStartDate: CStartDate,
        copyEndDate: CEndDate,
        activeStartDate: sdate,
        activeEndDate: edate,
      };
      const response = await postAPICall(
        "api/Schedule/CopySchedule",
        requestBody
      );
      fetchScheduleData();
      notifyMessage("Copy Schedule SuccessFully...", "success");
    } else {
      notifyMessage(
        "Please Select Library Id and StartDate & EndDate",
        "warning"
      );
    }
  };
  //get CSdate&&CEDate
  const onValueChanged = (e) => {
    setcopyStartDate(e.value[0]);
    setcopyEndDate(e.value[1]);
  };
  //get current date
  const onCurrentDateChange = (e) => {
    var curr = new Date(e); // get current date
    var first = curr.getDate() - curr.getDay(); // First day is the day of the month - the day of the week
    var last = first + 6; // last day is the first day + 6
    var firstday = new Date(curr.setDate(first));
    var lastday = new Date(curr.setDate(last));
    setActiveStartDate(firstday);
    setActiveEndDate(lastday);
  };
  //selcet check box
  const ChkboxhandleChange = (item) => {
    item === selItem ? setSelItem(null) : setSelItem(item);
  };

  const onAppointmentDblClick = (e) => {
    let schId = e.appointmentData.ScheduleHdrId;
    localStorage.setItem("SchId", schId);
    navigate("/playlist");
  };
  return (
    <div>
      <p hidden>{props.selectednewid}</p>
      <p id="schName">Name:{props.selectednewName}</p>
      <div id="headerContent">
        <DateRangeBox
          id="SchDateRange"
          width={300}
          height={40}
          multiView={false}
          onValueChanged={onValueChanged}
          ref={daterangeref}
        />

        <div id="SchCheckBox">
          <CheckBox
            defaultValue={true}
            onValueChange={() => ChkboxhandleChange("Tv1")}
            value={selItem === "Tv1"}
          />
          <label class="form-check-label" for="flexCheckIndeterminate">
            Tv1
          </label>
          <CheckBox
            defaultValue={true}
            onValueChange={() => ChkboxhandleChange("Tv2")}
            value={selItem === "Tv2"}
          />
          <label class="form-check-label" for="flexCheckIndeterminate">
            Tv2
          </label>
          <CheckBox
            defaultValue={true}
            onValueChange={() => ChkboxhandleChange("Tv3")}
            value={selItem === "Tv3"}
          />
          <label class="form-check-label" for="flexCheckIndeterminate">
            Tv3
          </label>
          <CheckBox
            defaultValue={true}
            onValueChange={() => ChkboxhandleChange("Tv4")}
            value={selItem === "Tv4"}
          />
          <label class="form-check-label" for="flexCheckIndeterminate">
            Tv4
          </label>
          <CheckBox
            defaultValue={true}
            onValueChange={() => ChkboxhandleChange("Tv5")}
            value={selItem === "Tv5"}
          />
          <label class="form-check-label" for="flexCheckIndeterminate">
            Tv5
          </label>
        </div>

        <Button
          id="cpyBtn"
          hint="Copy Date"
          icon="copy"
          onClick={CopySDatetoEData}
        ></Button>
      </div>
      <LoadPanel
        // shading={true}
        // shadingColor="rgba(0,0,0,0.4)"
        visible={loading}
        showIndicator={true}
        showPane={true}
      />
      <Scheduler
        // timeZone="India"
        id="dxscheduleData"
        // ref={gridRef}
        dataSource={scheduleData}
        views={views}
        defaultCurrentView="week"
        defaultCurrentDate={currentDate}
        startDayHour={0}
        endDayHour={24}
        remoteFiltering={true}
        DateRangeBox="daterange"
        onCurrentDateChange={onCurrentDateChange}
        dateSerializationFormat="yyyy-MM-ddTHH:mm:ss"
        textExpr="Subject"
        startDateExpr="StartDate"
        endDateExpr="EndDate"
        onAppointmentFormOpening={appoinmentFormOpening}
        onAppointmentDeleted={onAppointmentDelete}
        onAppointmentUpdated={onupdateAppointment}
        onAppointmentDblClick={onAppointmentDblClick}
      >
        <AppointmentDragging group="schgroup" onAdd={onAppointmentAdd} />
      </Scheduler>
    </div>
  );
}
