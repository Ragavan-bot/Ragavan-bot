import React, { useState } from "react";
import Header from "../Header/Header";
import Schedulectrl from "../Api/Scheduler";
import Split from "react-split";
import Tree from "../Api/Tree";
import PlaylistScheduleHeader from "../Api/PlaylistScheduleHeader";
import "../Schedule/Schedule.css";

export default function Schedule() {
  const [newid, SetNewid] = useState("");
  const [newname, SetNewName] = useState("");
  function onclicheck(value) {
    if (value !== undefined) getSelectedRowdatas(value);
  }
  function getSelectedRowdatas(selectedRowsData) {
    SetNewid(selectedRowsData.map((selectedValue) => selectedValue.ID));
    SetNewName(selectedRowsData.map((selectedValue) => selectedValue.Name));
  }

  return (
    <div>
      <Header />
      <Split className="split">
        <div id="SchTreeSplitter">
          <Tree onclickcheck={onclicheck} />
        </div>
        <div id="SchScheduleHeader">
          <PlaylistScheduleHeader
            selectednewid={"1"}
            selectednewName={"Schedule"}
            onclickcheck={onclicheck}
          />
        </div>

        <div id="SchScheduler">
          <Schedulectrl selectednewid={newid} selectednewName={newname} />
        </div>
      </Split>
    </div>
  );
}
