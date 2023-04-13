
//creates requirement with a name
export const createUnitAsync = async function (unitName, resourceId, resourceCapacity) {
    var myData = {};
    myData.UnitName = unitName;
    myData.ResourceId = resourceId;
    myData.ResourceCapacity = resourceCapacity;

    console.log(myData)

    let response = await fetch("http://localhost:5266/api/Unit", {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
}
export const updateResourceAmountAsync = async function (name, id, amount) {
    const myData = { Id: id, Amount: amount };
    console.log(myData);

    const response = await fetch(`http://localhost:5266/api/Unit/${name}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(myData),
    });
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
}

