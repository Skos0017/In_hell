import express from 'express';
import bodyParser from 'body-parser';
import bcrypt from 'bcrypt';
import fs from 'fs';
import path from 'path';
import express from 'express';
import http from 'http';
import { Server } from 'socket.io';

const app = express();
const PORT = process.env.PORT || 3000;

app.use(bodyParser.json());

//Путь хранения файла 

const usersFilePath = path.join(__dirname, 'Users.json');

//Запись файла в json

const writeUsersToFile = (users:any) =>{
    fs.writeFileSync(usersFilePath, JSON.stringify(users, null, 2));
};

// Функция для чтения пользователя 

const readUsersFromeFile = () =>{
    if(fs.existsSync(usersFilePath)){
        return [];
    }
    const data = fs.readFileSync(usersFilePath, 'utf-8');
    return JSON.parse(data);
};


// Регистрация 

app.post('/registr', async (req, res) => {
    const {username, password} = req.body;

    if(!username || !password){
        return res.status(400).json({massage:'Username and password are required'});
    }

    //хеш пароля
    const hashedPassword = await bcrypt.hash(password, 10);

    //new person
    const newUser = {username, password: hashedPassword};
    users.push(newUser);

    //запись файла
    writeUsersToFile(users);

    res.status(201).json({massage: 'User registr succefully'});
});

app.listen(PORT, () => { console.log("Server is running on http://localhost:${PORT}"); });


const server = http.createServer(app);
const io = new Server(server);



io.on('connection', (socket) => {
    console.log('New user connected');

    socket.on('sendMessage', (message) => {
        console.log('Message received:', message);
        io.emit('receiveMessage', message);
    });

    socket.on('disconnect', () => {
        console.log('User  disconnected');
    });
});

server.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});