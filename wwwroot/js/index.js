function CallApi() {
    // Invoke API elements
    const apitoken = document.getElementById("apitoken");
    const endpoint = document.getElementById("endpoint");
    const httpmethod = document.getElementById("httpmethod");
    const payload = document.getElementById("payload");

    // Response elements
    const responseHeaders = document.getElementById("responseHeaders");
    const responseBody = document.getElementById("responseBody");

    // Log and return if endpoint field is empty
    if (endpoint.value == "") {
        console.log("Error: Endpoint must be filled.");
        return;
    }

    // Setup default headers for json
    const headers = new Headers();
    headers.append("Accept", "application/json");
    headers.append("Content-Type", "application/json");

    // Add Authorization header if api token is provided
    if (apitoken != "") headers.append("Authorization", "Bearer " + apitoken.value);

    // Make api call
    fetch(endpoint.value, {
        method: httpmethod.value,
        headers: headers,
        body: (payload.value == "" || httpmethod.value == "GET") ? null : payload.value
    })
    .then(response => {
        responseHeaders.value = JSON.stringify({
            "Status": response.status,
            "StatusText": response.statusText,
            "Headers": response.headers,
            "Redirected": response.redirected
        }, null, 4);
        return response.json();
    })
    .then(data => {
        if (data.hasOwnProperty("token")) apitoken.value = data.token;
        responseBody.value = JSON.stringify(data, null, 4);
    })
    .catch(error => {
        responseBody.value = JSON.stringify(error, null, 4)
    });
}

// Set endpoint placeholder
document.getElementById("endpoint").placeholder = window.location.href;

// Attach onclick event listener on the "Call API" button
document.getElementById("callApiButton").onclick = function() {
    CallApi();
}

// Template 1 loader
document.getElementById("loadTemplate1Button").onclick = function() {
    const endpoint = document.getElementById("endpoint");
    const httpmethod = document.getElementById("httpmethod");
    const payload = document.getElementById("payload");
    httpmethod.value = "POST";
    endpoint.value = `${window.location.href}account`
    payload.value =
`{
    "AccountId": "myAccountId"
}`;
}

// Template 2 loader
document.getElementById("loadTemplate2Button").onclick = function() {
    const endpoint = document.getElementById("endpoint");
    const httpmethod = document.getElementById("httpmethod");
    const payload = document.getElementById("payload");
    endpoint.value = `${window.location.href}account/login`;
    httpmethod.value = "POST";
    payload.value =
`{
    "AccountId": "myAccountId",
    "Password": "password"
}`;
}

// Template 3 loader
document.getElementById("loadTemplate3Button").onclick = function() {
    const endpoint = document.getElementById("endpoint");
    const httpmethod = document.getElementById("httpmethod");
    const payload = document.getElementById("payload");
    endpoint.value = `${window.location.href}register`;
    httpmethod.value = "POST";
    payload.value =
`{
    "url": "https://jobs.smartrecruiters.com/Infobip/743999704152198-.net-software-engineer",
    "redirectType": 302
}`;
}

// Template 5 loader
document.getElementById("loadTemplate5Button").onclick = function() {
    const endpoint = document.getElementById("endpoint");
    const httpmethod = document.getElementById("httpmethod");
    endpoint.value = `${window.location.href}statistic/myAccountId`;
    httpmethod.value = "GET";
}
