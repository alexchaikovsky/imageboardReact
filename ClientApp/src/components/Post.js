import React from "react";

function getName(name) {
  if (name == null) return "Anonymous";
  return name;
}

function transformTime(datetime) {
  let dateObj = new Date(datetime).toLocaleString();
  return dateObj.split(",")[0] + dateObj.split(",")[1];
}

function insertImage(imageSource) {
  //if (imageSource == null) return;
  return (
    <a href={"image/" + imageSource}>
      <img
        //src={require(imageSource)}
        src={"image/" + imageSource}
        class="image"
        align="left"
        alt=""
      />
    </a>
  );
}

const Post = (data) => {
  console.log(data);
  return (
    <table>
      <tbody>
        <tr>
          <td class="reply">
            <a name={data.data.id}></a>
            <label>
              <input type="checkbox" name="delete" value={data.data.id} />
              <span class="replytitle"></span>
              <span class="commentpostername"> {getName(data.data.name)} </span>
              {transformTime(data.data.dateTime)}
            </label>
            <span class="reflink">
              <a> No. {data.data.id} </a>
            </span>
            &nbsp; <br />
            {data.data.imagesSource == null ? (
              <div></div>
            ) : (
              data.data.imagesSource.map((image) => insertImage(image))
            )}
            <blockquote class={"block"}>{data.data.text}</blockquote>
          </td>
        </tr>
      </tbody>
    </table>
  );
};
export default Post;
