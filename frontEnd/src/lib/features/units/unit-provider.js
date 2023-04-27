import { UnitsApiUrl } from "../../../constants";
//creates requirement with a name
export const createUnitAsync = async function (unitName, resourceId, resourceCapacity) {
    var myData = {};
    myData.UnitName = unitName;
    myData.ResourceId = resourceId;
    myData.ResourceCapacity = resourceCapacity;

    console.log(myData)

    let response = await fetch(UnitsApiUrl, {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
}
export const updateResourceAmountAsync = async function (name, id, amount) {
    const myData = { Id: id, Amount: amount };
    console.log(myData);

    const response = await fetch(`${UnitsApiUrl}/${name}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(myData),
    });
}

export const getPlansContainingAsync = async function (unitName) {
    let response = await fetch(`${UnitsApiUrl}/${unitName}/contained`, { method: 'GET' })
    const data = await response.json()
    return data;
}

export const addResourceAsync = async function (name, id, amount) {
    const myData = { Id: id, Amount: amount };
    console.log(myData)

    const response = await fetch(`${UnitsApiUrl}/${name}/addItem`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(myData),
    });
}

export const getUnitsAsync = async function () {
    let data = await fetch(UnitsApiUrl, { method: 'GET' })
    return await data.json()
}
export const getUnitAsync = async function (name) {
    let response = await fetch(`${UnitsApiUrl}/${name}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getUnitsQueryAsync = async function (name, returnList) {
    let response = await fetch(`${UnitsApiUrl}/${name}?returnList=${returnList}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deleteUnitAsync = async function (name) {
    let response = await fetch(`${UnitsApiUrl}/${name}`, { method: 'DELETE' })
}
export const deleteResourceFromUnitAsync = async function (name, resId) {
    let response = await fetch(`${UnitsApiUrl}/${name}/removeRes/${resId}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'DELETE',
    })
}

