// async function createRequirement(name) {
//     let response = await fetch('/api/Requirements', { method: 'POST', body: JSON.stringify({ name }) })
//     console.log(response)
//     return await response.json()
// }
function createRequirement(name) {
    console.log("poseru se z ")
    console.log(name)
    let response = fetch('/api/Requirements', { method: 'POST', body: JSON.stringify({ name }) })
    console.log(response)
    return response.json
}