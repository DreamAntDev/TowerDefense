var app = require('express')();
var server = require('http').createServer(app);
// http server를 socket.io server로 upgrade한다
var io = require('socket.io')(server);

// localhost:3000으로 서버에 접속하면 클라이언트로 index.html을 전송한다
app.get('/', function(req, res) {
  res.sendFile(__dirname + '/index.html');
});

var userId = 0;
// connection event handler
// connection이 수립되면 event handler function의 인자로 socket인 들어온다
io.on('connection', function(socket) {
  console.log("connection : " + socket.id);

  // 접속한 클라이언트의 정보가 수신되면
  socket.on('login', function(data) {
    if(data == 'MClient'){
      userId++;
      userId = userId % 100;
      data += userId;
    }
    console.log('Client logged-in:\n name:' + data);

    // socket에 클라이언트 정보를 저장한다
    socket.name = data;
  });


  // 클라이언트로부터의 메시지가 수신되면
  socket.on('Play', function(data) {
    console.log('Message from %s: %s', socket.name, data);

    /*io.emit('Play', {
      id: socket.name,
      msg: data
    });*/
    // 메시지를 전송한 클라이언트를 제외한 모든 클라이언트에게 메시지를 전송한다
    socket.broadcast.emit('Play', {
      id: socket.name,
      msg: data
    });

    // 메시지를 전송한 클라이언트에게만 메시지를 전송한다
     socket.emit('Play', {
       id: socket.name,
       msg: data
     });

    // 접속된 모든 클라이언트에게 메시지를 전송한다
    // io.emit('s2c chat', msg);

    // 특정 클라이언트에게만 메시지를 전송한다
    // io.to(id).emit('s2c chat', data);
  });

  //TowerDefence Event
  socket.on('Death', function(data) {
    console.log('TowerDefence %s: %s', socket.name, data);

    //if(Checking) Gold Up
    socket.emit('Death', {
      id: socket.name,
      msg: "OK"
    });
  });
  
  // force client disconnect from server
  socket.on('forceDisconnect', function() {
    socket.disconnect();
  })

  socket.on('disconnect', function() {
    console.log('user disconnected: ' + socket.name);
  });
});

server.listen(3000, function(){
  console.log('Socket IO sever listening on port 3000');
})
