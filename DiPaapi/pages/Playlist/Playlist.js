import Header from "../Header/Header";
import Split from "react-split";
import { useState } from "react";
import PlaylistLibraryGrid from "../Api/PlaylistLibraryGrid";
import PlaylistScheduleHeader from "../Api/PlaylistScheduleHeader";
import PlaylistScheduleList from "../Api/PlaylistScheduleList";
import LookupviewTree from "../Api/LookupviewTree";


import { DragDropContext } from "react-beautiful-dnd";
export default function Playlist() {
  const [newid, SetNewid] = useState("");
  const [newtreeid, SettreeNewid] = useState("");
  function onclicheck(value) {
    console.log(value, "selectedvalue");
    if (value != undefined) SetNewid(value);
  }
  function ontreeselection(value) {
    console.log(value, "selectedvalue");
    if (value != undefined) SettreeNewid(value);
  }
 
  return (
    <div>
      <Header />
      <DragDropContext  >
        <Split className="split">
          <div>
            <PlaylistScheduleHeader
              selectednewid={"1"}
              selectednewName={'Playlist'}
              onclickcheck={onclicheck}
            />
          </div>
          <div>
            <PlaylistScheduleList selectednewid={newid}></PlaylistScheduleList>
          </div>
          <div>
            <LookupviewTree ontreeselection={ontreeselection} />

            <PlaylistLibraryGrid
              selectednewid={newtreeid}
            ></PlaylistLibraryGrid>
          </div>
        </Split>
      </DragDropContext>
    </div>
  );
}
