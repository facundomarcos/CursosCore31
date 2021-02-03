import axios from 'axios';

//url base
axios.defaults.baseURL = 'http://localhost:5000/api';

//intercepta el token y agrega la autorizacion en el header
axios.interceptors.request.use((config) => {
    const token_seguridad = window.localStorage.getItem('token_seguridad');
    if(token_seguridad){
        //que se agregue en el header el token
        config.headers.Authorization = 'Bearer ' + token_seguridad;
        return config;
    }
}, error => {
    return Promise.reject(error);
});

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