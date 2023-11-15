import Header from "../Header/Header";
import Split from "react-split";
import Tree from "../Api/Tree";
import LibraryGrid from "../Api/LibraryGrid";
import { useState } from "react"; 

//StyleSheet
import "../Library/Library.css";
import  '../StyleSheet/StyleSheet.css'

const Library = () => {
  const [newid, SetNewid] = useState("");
  const [newname, SetNewName] = useState("");
  function onclicheck(value) {
    if (value != undefined) getSelectedRowdatas(value);
  }

  function getSelectedRowdatas(selectedRowsData) {
    SetNewid(selectedRowsData.map((selectedValue) => selectedValue.ID));
    SetNewName(selectedRowsData.map((selectedValue) => selectedValue.Name));
  }

  return (
    <div>
      <Header />
      <Split  className="split-container">
        <div  class="left-pane">
          <Tree onclickcheck={onclicheck} />
        </div>
        <div class="right-pane">
          {/* <LibraryFileUpload selectednewid={newid} /> */}
          <LibraryGrid selectednewid={newid} selectednewName={newname} /> 
        </div>
      </Split>
    </div>
  );
};

export default Library;
