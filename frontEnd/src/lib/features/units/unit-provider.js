
//creates requirement with a name
export const createUnitAsync = async function (unitName, resourceName, resourceCapacity) {
    var myData = {};
    myData.UnitName = unitName;
    myData.ResourceName = resourceName;
    myData.ResourceCapacity = resourceCapacity;

    console.log(myData)

    let response = await fetch("http://localhost:5266/api/Unit", {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
    const data = await response.json()
    return data
}

export const getUnitsAsync = async function () {
    let data = await fetch("http://localhost:5266/api/Unit", { method: 'GET' })
    return await data.json()
}
export const getUnitAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/Unit" + "/" + name, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getUnitsQueryAsync = async function (name, returnList) {
    let response = await fetch("http://localhost:5266/api/Unit" + "/" + name + "?returnList=" + returnList, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deleteUnitAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/Unit" + "/" + name, { method: 'DELETE' })
    const data = await response.json()
    return data;
}
// export const deleteRequirementAsync = async function (requirementName) {
//     let response = await fetch(RequirementsApiUrl + "/" + requirementName, { method: 'DELETE' })
//     const data = await response.json()
//     return data;
// }

