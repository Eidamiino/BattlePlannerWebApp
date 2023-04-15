
//creates requirement with a name
export const createPlanAsync = async function (planName, unitId, duration) {
    var myData = {};
    myData.PlanName = planName;
    myData.UnitId = unitId;
    myData.Duration = duration;

    console.log(myData)

    let response = await fetch("http://localhost:5266/api/BattlePlan", {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
}

export const getPlansAsync = async function () {
    let data = await fetch("http://localhost:5266/api/BattlePlan", { method: 'GET' })
    return await data.json()
}
export const getPlanAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/BattlePlan" + "/" + name, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getSummaryAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/BattlePlan" + "/" + name + "/summary", { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getPlansQueryAsync = async function (name, returnList) {
    let response = await fetch("http://localhost:5266/api/BattlePlan" + "/" + name + "?returnList=" + returnList, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deletePlanAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/BattlePlan" + "/" + name, { method: 'DELETE' })
}
export const addUnitAsync = async function (name, id) {
    const unitId = id;
    console.log(unitId)
    console.log(name)

    const response = await fetch(`http://localhost:5266/api/BattlePlan/${name}/addItem`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(unitId),
    });
}
// export const deleteRequirementAsync = async function (requirementName) {
//     let response = await fetch(RequirementsApiUrl + "/" + requirementName, { method: 'DELETE' })
//     const data = await response.json()
//     return data;
// }

