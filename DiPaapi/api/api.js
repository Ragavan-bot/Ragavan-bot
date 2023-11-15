import axios from "axios";
import notify from "devextreme/ui/notify";

export default function Globalapi() {
  return <div></div>;
}

// DEV
export const companyID = "1";
/* Dev Big City Urls */
//export const axiosbaseurl = "http://20.219.75.189:8091";
export const axiosapibaseurl = "http://localhost:5183/";

export const BAUsername = "DotnetDTV";
export const BAPassword = "DNDTV#092023";
const credentials = btoa(`${BAUsername}:${BAPassword}`);
export const  s3url = "https://hapbusinessportal.s3.ap-south-1.amazonaws.com/";
export const axiosDTVbasicAuthJson = {
  headers: {
    Authorization: `Basic ${credentials}`,
    "Content-Type": "application/json", // Modify content type as needed
  },
};
export const axiosDTVbasicAuthForm = {
  headers: {
    Authorization: `Basic ${credentials}`,
    "Content-Type": "multipart/form-data", // Modify content type as needed
  },
};
export const axiosDTVbasicDelete = {
  Authorization: `Basic ${credentials}`,
  "Content-Type": "application/json",
};

// Function to make a POST request
export const postAPICall = async (endpointURL, paramdata) => {
  try {
    const response = await axios.post(
      `${axiosapibaseurl}${endpointURL}`,
      paramdata,
      axiosDTVbasicAuthJson
    );
    return response;
  } catch (error) {
    throw error;
  }
};

// Function to make a POST request
export const putAPICall = async (endpointURL, paramdata) => {
  try {
    const response = await axios.put(
      `${axiosapibaseurl}${endpointURL}`,
      paramdata,
      axiosDTVbasicAuthJson
    );
    return response;
  } catch (error) {
    throw error;
  }
};

export const DeleteAPICall = async (endpointURL, paramdata) => {
  try {
    // headers and data key value must be present
    const response = await axios.delete(`${axiosapibaseurl}${endpointURL}`, {
      headers: axiosDTVbasicDelete,
      data: paramdata,
    });
    return response;
    console.log(response?.data);
  } catch (error) {
    debugger;
    console.error(error);
  }
};

export function notifyMessage(alertMessage,type) {
  debugger;
  const message = alertMessage;
  notify(
    {
      message,width:300,
      position: {
        my: "right top",
        at: "right top",
      },
      
    },
   type,
    3000
  );
}

