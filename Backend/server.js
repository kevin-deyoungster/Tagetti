const express = require("express");
const bodyParser = require("body-parser");
const MongoClient = require("mongodb").MongoClient;
const cors = require("cors");
require("dotenv").config();

const corsOptions = {
    origin: "*",
    methods: ["GET", "HEAD", "PUT", "PATCH", "POST", "DELETE", "OPTIONS"],
    credentials: true,
    allowedHeaders: ["Accept", "X-Access-Token", " X-Application-Name", " X-Request-Sent-Time"]
};

const app = express();
const port = process.env.PORT || 8000;

app.use(
    bodyParser.urlencoded({
        extended: true
    })
);

app.use(cors(corsOptions));

app.use(express.static(__dirname + "/views"));
app.set("view engine", "ejs");

MongoClient.connect(process.env.MONGO_URI, (err, database) => {
    if (err) return console.log(err);
    db = database;
    app.listen(port, function() {
        console.log("Listening on " + port);
    });
});

app.get("/admin", function(req, res) {
    res.sendFile("views/admin.html", {
        root: __dirname
    });
});

// Function to create user. Checks if user exists before. If username already exists returns false. If Successfully true
app.get("/create/:name/:score", function(req, res) {
    (Tname = req.params.name), (Tscore = req.params.score);

    var cursor = db
        .collection("Users")
        .find({
            Name: Tname
        })
        .toArray(function(err, items) {
            console.log(items);
            if (items.length > 0) {
                res.send(false);
            } else {
                db.collection("Users").save(
                    {
                        Name: Tname,
                        Score: Tscore
                    },
                    (err, result) => {
                        if (err) return console.log(err);
                        console.log("Created account " + Tname);
                        res.send(true);
                    }
                );
            }
        });
});

//  [Return = json of  players]
app.get("/leaderboard", function(req, res) {
    response = "";

    var cursor = db
        .collection("Users")
        .find()
        .toArray(function(err, results) {
            if (results.length != 0) {
                console.log(`Pulled Leaderboard ${results.length} players`);
                players = [];

                //Make an array of players and their scores
                for (var i = 0; i < results.length; i++) {
                    players.push({
                        Name: results[i].Name,
                        Score: results[i].Score
                    });
                }

                //Arrange in ascending order
                players.sort(function(a, b) {
                    return b.Score - a.Score;
                });

                // //Correct the limit: When user asks for number of players more than the players in the databse
                // if(results.length < limit){
                //   limit = results.length
                // }

                //Loop through and make a response
                for (i = 0; i < results.length; i++) {
                    // console.log("Player " + (Number(i) + Number(1)) + ": " + players[i].Name + " " + players[i].Score);
                    response += players[i].Name + "*" + players[i].Score + "|";
                }

                res.send(response);
            } else {
                res.send(" ");
            }
        });
});

// Function to update a players score
app.get("/update/:name/:score", function(req, res) {
    Tname = req.params.name;
    Tscore = req.params.score;
    db.collection("Users").update(
        {
            Name: Tname
        },
        {
            $set: {
                Score: Tscore
            }
        },
        (err, result) => {
            if (err) {
                console.log(err);
                res.send(false);
            } else {
                console.log("Successfully updated" + Tname + "with new score" + Tscore);
                res.send(true);
            }
        }
    );
});

//Function to delete player
app.get("/remove/:name", function(req, res) {
    Tname = req.params.name;
    db.collection("Users").remove(
        {
            Name: Tname
        },
        function(err, result) {
            if (err) {
                console.log(err);
                res.send(false);
            } else {
                console.log(`Removed Player ${Tname}`);
                console.log(result);
                res.send(true);
            }
        }
    );
});
