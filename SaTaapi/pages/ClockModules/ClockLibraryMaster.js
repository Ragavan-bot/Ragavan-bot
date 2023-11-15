import React from "react";
import Split from "react-split-grid";
import SchedulerHome from "../SchedulerHome/SchedulerHome";
import ProgramTemplateDetail from "./ProgramTemplateDetail.js";
import TabPanel, { Item } from "devextreme-react/tab-panel";
import "./ClockLibraryMaster.css";
import ProgramTemplateHeader from "./ProgramTemplateHeader.js";
import LibraryTreeList from "./LibraryTreeList.js";
function ClockLibraryMaster() {
  return (
    <div id="SplitContainer">
      <SchedulerHome />
      <Split minSize={100} cursor="col-resize">
        {({ getGridProps, getGutterProps }) => (
          <div>
            <div className="Spliter" {...getGridProps()}>
              <div>
                <TabPanel id="TabPanelClockMaster" height={600}>
                  <Item title="ProgramTemplateHeader">
                    <div>
                      <ProgramTemplateHeader />
                    </div>
                  </Item>
                  <Item title="LibraryTreeList">
                    <div>
                      <LibraryTreeList />
                    </div>
                  </Item>
                </TabPanel>
              </div>
              <div className="SecondSplit2" {...getGutterProps("column", 1)} />
              <div id="spliterdata">
                <ProgramTemplateDetail />
              </div>
            </div>
          </div>
        )}
      </Split>
    </div>
  );
}
export default ClockLibraryMaster;
