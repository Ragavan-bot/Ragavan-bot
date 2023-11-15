import React, { useEffect, useState } from "react";
import TreeList, {
  RemoteOperations,
  Column,
  SearchPanel,
  HeaderFilter,
  RequiredRule,
  Selection,
} from "devextreme-react/tree-list";
import { postAPICall } from "../../api/api";
//StyleSheet
import "../StyleSheet/StyleSheet.css";
import { LoadPanel } from "devextreme-react";

export default function Tree(props) {
  const [TreeData, setTreeData] = useState([]);
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
    fetchScheduleData();
    simulateLoading();
  }, [props]);

  const fetchScheduleData = async () => {
    const requestBody = {
      companyID: "1",
    };
    let response = await postAPICall(
      "api/Global/HAPLiveHierarchy",
      requestBody
    );
    if (response?.status == 200) {
      setTreeData(response?.data);
    } else {
      alert("no data found");
      setTreeData([]);
    }
  };
  const onSelectionChange = (e) => {
    if (e.selectedRowsData.length > 0) props.onclickcheck(e.selectedRowsData);
  };
  const handleContentReady = (e) => {
    var treeList = e.component;
    var allRows = treeList.getVisibleRows();
    var expandPromises = allRows.map(function (row) {
      return treeList.expandRow(row.key);
    });
    Promise.all(expandPromises);
  };
  return (
    <div>
       <LoadPanel
        // shading={true}
        // shadingColor="rgba(0,0,0,0.4)"
        visible={loading}
        showIndicator={true}
        showPane={true}
      />
    
    <TreeList
      id="treeList"
      dataSource={TreeData}
      keyExpr="ID"
      parentIdExpr="ParentID"
      
      //onContentReady={handleContentReady}
      autoExpandAll={handleContentReady}
      onSelectionChanged={(e) => {
        onSelectionChange(e);
      }}
    >
      <RemoteOperations filtering={true} sorting={true} grouping={true} />
      <SearchPanel visible={true} width={300} />
      <HeaderFilter visible={true} />
      <Selection mode="single" />
      <Column dataField="Name" minWidth={120}>
        <RequiredRule />
      </Column>
    </TreeList>
    </div>
  );
}
