import { ResourcesApiUrl } from "../../../constants";
//creates requirement with a name
export const createResourceAsync = async function (resourceName, requirementId, requirementCapacity) {
    var myData = {};
    myData.ResourceName = resourceName;
    myData.RequirementId = requirementId;
    myData.RequirementCapacity = requirementCapacity;

    console.log(myData)

    let response = await fetch(ResourcesApiUrl, {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
}
export const updateRequirementAmountAsync = async function (name, id, amount) {
    const myData = { Id: id, Amount: amount };
    console.log(myData);

    const response = await fetch(`${ResourcesApiUrl}/${name}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(myData),
    });
}

export const getUnitsContainingAsync = async function (resourceName) {
    let response = await fetch(`${ResourcesApiUrl}/${resourceName}/contained`, { method: 'GET' })
    const data = await response.json()
    return data;
}

export const addRequirementAsync = async function (name, id, amount) {
    const myData = { Id: id, Amount: amount };
    console.log(myData)

    const response = await fetch(`${ResourcesApiUrl}/${name}/addItem`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(myData),
    });
}


export const getResourcesAsync = async function () {
    let data = await fetch(ResourcesApiUrl, { method: 'GET' })
    return await data.json()
}
export const getResourceAsync = async function (resourceName) {
    let response = await fetch(`${ResourcesApiUrl}/${resourceName}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getResourcesQueryAsync = async function (resourceName, returnList) {
    let response = await fetch(`${ResourcesApiUrl}/${resourceName}?returnList=${returnList}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deleteResourceAsync = async function (name) {
    console.log("deleting fr " + name);
    let response = await fetch(`${ResourcesApiUrl}/${name}`, { method: 'DELETE' })
}

export const deleteRequirementFromResourceAsync = async function (name, reqId) {
    let response = await fetch(`${ResourcesApiUrl}/${name}/removeReq/${reqId}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'DELETE',
    })
}