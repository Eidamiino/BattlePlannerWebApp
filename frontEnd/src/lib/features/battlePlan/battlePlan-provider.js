import { PlansApiUrl } from "../../../constants";
//creates requirement with a name
export const createPlanAsync = async function (planName, unitId, duration) {
    var myData = {};
    myData.PlanName = planName;
    myData.UnitId = unitId;
    myData.Duration = duration;

    console.log(myData)

    let response = await fetch(PlansApiUrl, {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
}

export const getPlansAsync = async function () {
    let data = await fetch(PlansApiUrl, { method: 'GET' })
    return await data.json()
}
export const getPlanAsync = async function (name) {
    let response = await fetch(`${PlansApiUrl}/${name}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getSummaryAsync = async function (name) {
    let response = await fetch(`${PlansApiUrl}/${name}/summary`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getPlansQueryAsync = async function (name, returnList) {
    let response = await fetch(`${PlansApiUrl}/${name}?returnList=${returnList}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deletePlanAsync = async function (name) {
    let response = await fetch(`${PlansApiUrl}/${name}`, { method: 'DELETE' })
}
export const addUnitAsync = async function (name, id) {
    const unitId = id;
    console.log(unitId)
    console.log(name)

    const response = await fetch(`${PlansApiUrl}/${name}/addItem`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(unitId),
    });
}
export const deleteUnitFromPlanAsync = async function (name, unitId) {
    console.log("removing stuff", name, unitId);
    let response = await fetch(`${PlansApiUrl}/${name}/removeItem/${unitId}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'DELETE',
    })
}


