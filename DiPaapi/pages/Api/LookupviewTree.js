import React, { useState, useEffect } from "react";
import DropDownBox, { Position } from "devextreme-react/drop-down-box"; 
import TreeList, {
  Selection,
  Column,
  RequiredRule,
  SearchPanel,
  FilterRow,
} from "devextreme-react/tree-list";
import { postAPICall } from "../../api/api";

const requestBody = {
  companyID: "1",
}; 

const response=await postAPICall('api/Global/HAPLiveHierarchy',requestBody);

export default function LookupviewTree(props) {
  const [treeValue, setValue] = useState("Select category");
  const [isTreeBoxOpened, setIsTreeBoxOpened] = useState(false); 
  useEffect(() => {
    setIsTreeBoxOpened(false); 
  }, [isTreeBoxOpened]);

  function onSelectionChanged(e) {
    setIsTreeBoxOpened(true);
    let stringconversion = e?.selectedRowsData[0].Name.toString() || "";
    let gettreeID = e?.selectedRowsData[0].ID.toString() || "";
    setValue(stringconversion);   
   if (gettreeID != undefined) props.ontreeselection(gettreeID); 
  }

  function treeViewRender() {
    return (
      <TreeList
        dataSource={response.data}
        keyExpr="ID"
        displayExpr="Name"
        parentIdExpr="ParentID"
        height={700}
        dataStructure="plain"
        autoExpandAll={true}
        selectByClick={true}
        onSelectionChanged={onSelectionChanged}
        stylingMode="outlined"
      >
        <Selection mode="single" />
        <SearchPanel visible={true} highlightCaseSensitive={true} width={200}/>   
        <FilterRow visible={true} />
        <Column dataField="Name" caption="Name">
          <RequiredRule />
        </Column>
      </TreeList>
    );
  }

  return (
    <div className="contain"> 
      <DropDownBox
        opened={isTreeBoxOpened}
        dataSource={response.data}
        // width={400}
        showClearButton={true}
        showDropDownButton={true}
        placeholder={treeValue}
        contentRender={treeViewRender}
      ></DropDownBox>
    </div>
  );
}
