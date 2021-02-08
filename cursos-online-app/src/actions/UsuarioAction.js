import HttpCliente from '../servicios/HttpCliente';

//cuando hay procesos asincronos se usa una funcion, una promesa
//para esperar lo que me manda el servidor
//indica que la funcion no va a terminar hasta que el servidor envie la data
export const registrarUsuario = usuario => {
    return new Promise ( (resolve, eject) => {
        HttpCliente.post('/usuario/registrar', usuario).then(response => {
            resolve(response);
        })
    })
}


//para obtener los datos del usuario
//no recibe parametros, el token ya esta agregado en la peticion
//dispach -> axios se conecta al servidor
export const obtenerUsuarioActual = (dispach) =>{
    return new Promise( (resolve, eject) => {
        HttpCliente.get('/usuario').then(response => {
            dispach({
                type: "INICIAR_SESION",
                sesion : response.data,
                autenticado : true
            });
            resolve(response);
        })
    })
}

//actualizar usuario
export const actualizarUsuario = (usuario) => {
    return new Promise( (resolve, eject) => {
        HttpCliente.put('/usuario', usuario).then(response => {
            resolve(response);
        })
    })
}

//action login
export const loginUsuario = usuario => {
    return new Promise( (resolve, eject) => {
        HttpCliente.post('/usuario/login', usuario).then(response => {
            resolve(response);
        })
    })
}