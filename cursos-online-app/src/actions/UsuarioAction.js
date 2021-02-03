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