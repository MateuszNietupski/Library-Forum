import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { ENDPOINTS } from '../../utils/consts';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';


const Home = () => {
    const [news, setNews] = useState([]);
    const [selectedNews, setSelectedNews] = useState(null);
    const [show, setShow] = useState(false);

    useEffect(() => {
        axios.get(`${ENDPOINTS.GETNEW}`)
            .then(response => {
                setNews(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the news!', error);
            });
    }, []);
    const handleClose = () => setShow(false);
    const handleShow = (newsItem) => {
        setSelectedNews(newsItem);
        setShow(true);
    }
    return (
        <div className="main-board">
        <h1>Jakies Newsy</h1>
        <div className="news-container">
            {news.map((newsItem, index) => (
                <div key={index} className="news-item" onClick={() => handleShow(newsItem)}>
                    <h2>{newsItem.title}</h2>
                    <p>{newsItem.title}</p>
                </div>
            ))}
        </div>

        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
              <Modal.Title>{selectedNews?.title}</Modal.Title>
            </Modal.Header>
            <Modal.Body>{selectedNews?.content}</Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={handleClose}>
                Close
              </Button>
            </Modal.Footer>
        </Modal>
    </div>
    )

};

export default Home;