
//para obtener la extension del archivo de imagen
//la funcion recibe una imagen

export const obtenerDataImagen = imagen => {
    return new Promise((resolve, reject) => {
        const nombre = imagen.name;
        //que divida todos los "." y tome la ultima parte (extension)
        const extension = imagen.name.split(".").pop();

        const lector = new FileReader();
        lector.readAsDataURL(imagen);
        //manda un json con la data que necesita para guardar en el servidor
        lector.onload = () => resolve(
            {
                //separar la posicion de la coma y que lo separe un arreglo y tome la posicion 1 (el base64)
               data: lector.result.split(",")[1],
               nombre : nombre,
               extension : extension
            });

        lector.onerror = error => PromiseRejectionEvent(error);
    })
}