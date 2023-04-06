
//creates requirement with a name
export const createPlanAsync = async function (planName, unitName, unitCapacity) {
    var myData = {};
    myData.PlanName = planName;
    myData.UnitName = unitName;
    myData.UnitCapacity = unitCapacity;

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
export const getPlansQueryAsync = async function (name, returnList) {
    let response = await fetch("http://localhost:5266/api/BattlePlan" + "/" + name + "?returnList=" + returnList, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deletePlanAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/BattlePlan" + "/" + name, { method: 'DELETE' })
}
// export const deleteRequirementAsync = async function (requirementName) {
//     let response = await fetch(RequirementsApiUrl + "/" + requirementName, { method: 'DELETE' })
//     const data = await response.json()
//     return data;
// }

