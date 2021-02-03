import axios from 'axios';

//url base
axios.defaults.baseURL = 'http://localhost:5000/api';

//objeto generico para representar los request que se envian al servidor
const requestGenerico = {
    //envia una peticion y recibe una respuesta
    get : (url) => axios.get(url),
    //envia una peticion con parametros y recibe una respuesta
    post : (url, body) => axios.post(url, body),
    put : (url, body) => axios.put(url, body),
    //envia una peticion y los parametros estan en la url y recibe una respuesta
    delete : (url) => axios.delete(url),
};

export default requestGenerico;