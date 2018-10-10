const jsonServer = require('json-server');
const fs = require('fs');
const pause = require('connect-pause');
const server = jsonServer.create();
const middleware = jsonServer.defaults();
const path = require('path');
const router = jsonServer.router("data.json")
var jsonConcat = require('json-concat');

server.use(jsonServer.bodyParser)

server.use(middleware);

router.render = (req, res) => {
    res.jsonp({
        Result: res.locals.data,
        Status: 0,
        SystemMessage: "",
        FriendlyMessage: ""
    })
}

server.post('/login', function (req, res, next) {

    if(req.body.Email === "mock@test.com")
    {
        res.jsonp({
            Result: "mocktokenforapplication",
            Status: 0,
            SystemMessage: "",
            FriendlyMessage: ""
        })
    }
    else
     {  
        res.status(401).jsonp({
            error: "Unauthorized please use mock@test.com"
        });
     }  
});

server.use(function (req, res, next) {

  if (req.method === 'POST') {
    // Converts POST to GET and move payload to query params
    // This way it will make JSON Server that it's GET request
    req.method = 'GET'
    req.query = req.body
  }
  // Continue to JSON Server router
  next()
});

var routes = JSON.parse(fs.readFileSync("routes.json"));
server.use(jsonServer.rewriter(routes));

router.db._.id = "Id";
server.use(router);
server.use(pause(2000));

jsonConcat({
    src: "data",
    dest: "data.json",
}, function(){
    server.listen(3000, () => {
        console.log("mock server is running");
    });
});