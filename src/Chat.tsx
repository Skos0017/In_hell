// src/components/Chat.tsx
import React, { useEffect, useState } from 'react';
import { io } from 'socket.io-client';

const socket = io('http://localhost:3000'); // Убедитесь, что адрес соответствует вашему серверу

const Chat: React.FC = () => {
    const [messages, setMessages] = useState<string[]>([]);
    const [messageInput, setMessageInput] = useState<string>('');

    useEffect(() => {
        // Обработка входящих сообщений
        socket.on('receiveMessage', (message: string) => {
            setMessages((prevMessages) => [...prevMessages, message]);
        });

        // Очистка при размонтировании компонента
        return () => {
            socket.off('receiveMessage');
        };
    }, []);

    const sendMessage = () => {
        if (messageInput.trim()) {
            socket.emit('sendMessage', messageInput);
            setMessageInput('');
        }
    };

    return (
        <div>
            <h1>Chat Application</h1>
            <div>
                {messages.map((message, index) => (
                    <p key={index}>{message}</p>
                ))}
            </div>
            <input
                type="text"
                value={messageInput}
                onChange={(e) => setMessageInput(e.target.value)}
                placeholder="Type your message"
            />
            <button onClick={sendMessage}>Send</button>
        </div>
    );
};

export default Chat;