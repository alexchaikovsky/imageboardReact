import React from "react";

function getName(name) {
  if (name == null) return "Anonymous";
  return name;
}

function transformTime(datetime) {
  let dateObj = new Date(datetime).toLocaleString();
  return dateObj.split(",")[0] + dateObj.split(",")[1];
}

const Post = (data) => {
  console.log(data);
  return (
    <table>
      <tbody>
        <tr>
          <td class="reply" id="reply5066">
            <a name={data.data.id}></a>
            <label>
              <input type="checkbox" name="delete" value={data.data.id} />
              <span class="replytitle"></span>
              <span class="commentpostername">{getName(data.data.name)}</span>
              {transformTime(data.data.dateTime)}
            </label>
            <span class="reflink">
              <a>No. {data.data.id}</a>
            </span>
            &nbsp; <br />
            <blockquote>{data.data.text}</blockquote>
          </td>
        </tr>
      </tbody>
    </table>
  );
};
export default Post;
